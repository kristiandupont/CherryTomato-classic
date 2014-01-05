// This is the main DLL file.

#include "stdafx.h"

#include "CherryFirewall.h"
#include "CherryFirewallSettingsPanel.h"

using namespace System::Xml;

namespace CherryFirewall
{

CherryFirewall::CherryFirewall() : enabled_(true)
{
	blockAddresses = gcnew System::Collections::Generic::List<BlockAddress^>;
}

System::Windows::Forms::Control^ CherryFirewall::CreateSettingsPanel() 
{ 
	return gcnew CherryFirewallSettingsPanel(this); 
}

void CherryFirewall::LoadConfiguration(System::Xml::XmlElement^ fromElement)
{
    if (fromElement->GetAttribute("name") != PluginName)
        throw gcnew Exception("name attribute is not " + PluginName);

	enabled_ = bool::Parse(fromElement->GetAttribute("enabled"));

	XmlNodeList^ addressesNodes = fromElement->SelectNodes("addressesToBlock/address");
    for each (XmlElement^ addressesElement in addressesNodes)
    {
        String^ url = addressesElement->GetAttribute("url");
		AddAddress(url);
    }
}

void CherryFirewall::SaveConfiguration(System::Xml::XmlElement^ parentElement)
{
	XmlElement^ pluginElement = parentElement->OwnerDocument->CreateElement("plugin");
    parentElement->AppendChild(pluginElement);
	pluginElement->SetAttribute("name", PluginName);
	pluginElement->SetAttribute("enabled", enabled_ ? "True" : "False");

	XmlElement^ addressesToBlockElement = parentElement->OwnerDocument->CreateElement("addressesToBlock");
    pluginElement->AppendChild(addressesToBlockElement);
	
	for each (BlockAddress^ ba in blockAddresses)
	{
		XmlElement^ addressElement = parentElement->OwnerDocument->CreateElement("address");
		addressesToBlockElement->AppendChild(addressElement);
		addressElement->SetAttribute("url", ba->address);
	}
}

void CherryFirewall::StartPomodoro(System::TimeSpan timeRemaining)
{
	if(!enabled_) return;
	if(!IsCompatible()) return;

	packetFilter_ = new PacketFilter();
	
	for each(BlockAddress^ ba in blockAddresses)
	{
		try
		{
			if(ba->ipHostEntry == nullptr)
				ba->ipHostEntry = System::Net::Dns::GetHostEntry(ba->address);

			for each(System::Net::IPAddress^ ip in ba->ipHostEntry->AddressList)
			{
				String^ addr = ip->ToString();
				const wchar_t* chars = (const wchar_t*)(System::Runtime::InteropServices::Marshal::StringToHGlobalUni(addr)).ToPointer();
				packetFilter_->AddToBlockList(chars);
			}
		}
		catch(System::Exception^) {};
	}

	packetFilter_->StartFirewall();
}

void CherryFirewall::StopPomodoro(bool completed)
{
	delete packetFilter_;
}

// Only Windows Vista, Windows Server 2008 or higher is supported.
bool CherryFirewall::IsCompatible()
{
	return System::Environment::OSVersion->Version->Major >= 6;
}

void CherryFirewall::AddAddress(System::String^ address)
{
	blockAddresses->Add(gcnew BlockAddress(address));
}

void CherryFirewall::ChangeAddress(System::String^ address, System::String^ newAddress)
{
	for each (BlockAddress^ ba in blockAddresses)
	{
		if(ba->address == address)
		{
			ba->address = newAddress;
			return;
		}
	}
	throw gcnew Exception(address + " was not a registered url (trying to change to " + newAddress + ")");
}

void CherryFirewall::DeleteAddress(System::String^ address)
{
	for (int i = 0; i != blockAddresses->Count; ++i)
	{
		if(blockAddresses[i]->address == address)
		{
			blockAddresses->RemoveAt(i);
			return;
		}
	}
	throw gcnew Exception(address + " was not a registered url (trying to delete it)");
}

}
// CherryFirewall.h

#pragma once

#include "PacketFilter.h"

namespace CherryFirewall {

	[CherryTomato::Core::Plugin]
	public ref class CherryFirewall : 
		public CherryTomato::Core::IConfigurable,
		public CherryTomato::Core::IPomodoroAware
	{
	public:
		CherryFirewall();

		property System::String^ PluginName { virtual System::String^ get() { return "CherryFirewall"; } }
		property System::Drawing::Icon^ PluginIcon { virtual System::Drawing::Icon^ get() { return nullptr; } }

		virtual System::Windows::Forms::Control^ CreateSettingsPanel();

		virtual void LoadConfiguration(System::Xml::XmlElement^);
		virtual void SaveConfiguration(System::Xml::XmlElement^);

		virtual void StartPomodoro(System::TimeSpan timeRemaining);
		virtual void StopPomodoro(bool completed);

		bool IsCompatible();

		void AddAddress(System::String^ address);
		void ChangeAddress(System::String^ address, System::String^ newAddress);
		void DeleteAddress(System::String^ address);

		ref class BlockAddress
		{
		public:
			System::String^ address;
			System::Net::IPHostEntry^ ipHostEntry;

			BlockAddress(System::String^ url) { this->address = url; }
		};

		System::Collections::Generic::List<BlockAddress^>^ blockAddresses;

	private:
		PacketFilter* packetFilter_;
		bool enabled_;
	};
}

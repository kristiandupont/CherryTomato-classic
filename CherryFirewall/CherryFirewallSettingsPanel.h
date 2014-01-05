#pragma once

#include "CherryFirewall.h"
#include "EditAddressDialog.h"

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;


namespace CherryFirewall {

	/// <summary>
	/// Summary for CherryFirewallSettingsPanel
	/// </summary>
	public ref class CherryFirewallSettingsPanel : public System::Windows::Forms::UserControl
	{
	public:
		CherryFirewallSettingsPanel(CherryFirewall^ cherryFirewall)
		{
			cherryFirewall_ = cherryFirewall;
			InitializeComponent();

			bool compatible = cherryFirewall->IsCompatible();
			vistaRequiredLabel->Visible = !compatible;
			addressesToBlockLabel->Enabled = compatible;
			addressesListView->Enabled = compatible;
			addAddressButton->Enabled = compatible;

			for each(CherryFirewall::BlockAddress^ ba in cherryFirewall_->blockAddresses)
			{
				addressesListView->Items->Add(ba->address);
			}
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~CherryFirewallSettingsPanel()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::GroupBox^  cherryFirewallGroupBox;
	private: CherryFirewall^ cherryFirewall_;
	private: System::Windows::Forms::Label^  addressesToBlockLabel;

	private: System::Windows::Forms::Button^  addAddressButton;

	private: System::Windows::Forms::Label^  vistaRequiredLabel;
	private: System::Windows::Forms::ContextMenuStrip^  addressListContextMenuStrip;
	private: System::Windows::Forms::ToolStripMenuItem^  editUrlToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^  removeUrlToolStripMenuItem;

	private: System::Windows::Forms::ListView^  addressesListView;
	private: System::Windows::Forms::ColumnHeader^  urlColumnHeader;

	private: System::ComponentModel::IContainer^  components;

	protected: 

	protected: 

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>


#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->components = (gcnew System::ComponentModel::Container());
			this->cherryFirewallGroupBox = (gcnew System::Windows::Forms::GroupBox());
			this->vistaRequiredLabel = (gcnew System::Windows::Forms::Label());
			this->addAddressButton = (gcnew System::Windows::Forms::Button());
			this->addressesToBlockLabel = (gcnew System::Windows::Forms::Label());
			this->addressesListView = (gcnew System::Windows::Forms::ListView());
			this->urlColumnHeader = (gcnew System::Windows::Forms::ColumnHeader());
			this->addressListContextMenuStrip = (gcnew System::Windows::Forms::ContextMenuStrip(this->components));
			this->editUrlToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->removeUrlToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->cherryFirewallGroupBox->SuspendLayout();
			this->addressListContextMenuStrip->SuspendLayout();
			this->SuspendLayout();
			// 
			// cherryFirewallGroupBox
			// 
			this->cherryFirewallGroupBox->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom) 
				| System::Windows::Forms::AnchorStyles::Left) 
				| System::Windows::Forms::AnchorStyles::Right));
			this->cherryFirewallGroupBox->Controls->Add(this->vistaRequiredLabel);
			this->cherryFirewallGroupBox->Controls->Add(this->addAddressButton);
			this->cherryFirewallGroupBox->Controls->Add(this->addressesToBlockLabel);
			this->cherryFirewallGroupBox->Controls->Add(this->addressesListView);
			this->cherryFirewallGroupBox->Location = System::Drawing::Point(3, 3);
			this->cherryFirewallGroupBox->Name = L"cherryFirewallGroupBox";
			this->cherryFirewallGroupBox->Size = System::Drawing::Size(348, 208);
			this->cherryFirewallGroupBox->TabIndex = 0;
			this->cherryFirewallGroupBox->TabStop = false;
			this->cherryFirewallGroupBox->Text = L"Cherry Firewall";
			// 
			// vistaRequiredLabel
			// 
			this->vistaRequiredLabel->Anchor = System::Windows::Forms::AnchorStyles::None;
			this->vistaRequiredLabel->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(255)), 
				static_cast<System::Int32>(static_cast<System::Byte>(210)), static_cast<System::Int32>(static_cast<System::Byte>(210)));
			this->vistaRequiredLabel->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->vistaRequiredLabel->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.5F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(0)));
			this->vistaRequiredLabel->ForeColor = System::Drawing::Color::Red;
			this->vistaRequiredLabel->Location = System::Drawing::Point(17, 47);
			this->vistaRequiredLabel->Name = L"vistaRequiredLabel";
			this->vistaRequiredLabel->Size = System::Drawing::Size(313, 116);
			this->vistaRequiredLabel->TabIndex = 3;
			this->vistaRequiredLabel->Text = L"Sorry, this plugin only works with\r\nVista, Windows Server 2008 or higher";
			this->vistaRequiredLabel->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// addAddressButton
			// 
			this->addAddressButton->Location = System::Drawing::Point(239, 179);
			this->addAddressButton->Name = L"addAddressButton";
			this->addAddressButton->Size = System::Drawing::Size(102, 23);
			this->addAddressButton->TabIndex = 2;
			this->addAddressButton->Text = L"A&dd Address...";
			this->addAddressButton->UseVisualStyleBackColor = true;
			this->addAddressButton->Click += gcnew System::EventHandler(this, &CherryFirewallSettingsPanel::addAddressButton_Click);
			// 
			// addressesToBlockLabel
			// 
			this->addressesToBlockLabel->AutoSize = true;
			this->addressesToBlockLabel->Location = System::Drawing::Point(7, 20);
			this->addressesToBlockLabel->Name = L"addressesToBlockLabel";
			this->addressesToBlockLabel->Size = System::Drawing::Size(182, 13);
			this->addressesToBlockLabel->TabIndex = 0;
			this->addressesToBlockLabel->Text = L"&Addresses to block during pomodoro:";
			// 
			// addressesListView
			// 
			this->addressesListView->Columns->AddRange(gcnew cli::array< System::Windows::Forms::ColumnHeader^  >(1) {this->urlColumnHeader});
			this->addressesListView->FullRowSelect = true;
			this->addressesListView->HeaderStyle = System::Windows::Forms::ColumnHeaderStyle::Nonclickable;
			this->addressesListView->Location = System::Drawing::Point(6, 36);
			this->addressesListView->MultiSelect = false;
			this->addressesListView->Name = L"addressesListView";
			this->addressesListView->Size = System::Drawing::Size(335, 137);
			this->addressesListView->TabIndex = 4;
			this->addressesListView->UseCompatibleStateImageBehavior = false;
			this->addressesListView->View = System::Windows::Forms::View::Details;
			this->addressesListView->MouseClick += gcnew System::Windows::Forms::MouseEventHandler(this, &CherryFirewallSettingsPanel::addressesListView_MouseClick);
			// 
			// urlColumnHeader
			// 
			this->urlColumnHeader->Text = L"Url";
			this->urlColumnHeader->Width = 331;
			// 
			// addressListContextMenuStrip
			// 
			this->addressListContextMenuStrip->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(2) {this->editUrlToolStripMenuItem, 
				this->removeUrlToolStripMenuItem});
			this->addressListContextMenuStrip->Name = L"addressListContextMenuStrip";
			this->addressListContextMenuStrip->Size = System::Drawing::Size(136, 48);
			// 
			// editUrlToolStripMenuItem
			// 
			this->editUrlToolStripMenuItem->Name = L"editUrlToolStripMenuItem";
			this->editUrlToolStripMenuItem->Size = System::Drawing::Size(135, 22);
			this->editUrlToolStripMenuItem->Text = L"&Edit Url...";
			this->editUrlToolStripMenuItem->Click += gcnew System::EventHandler(this, &CherryFirewallSettingsPanel::editUrlToolStripMenuItem_Click);
			// 
			// removeUrlToolStripMenuItem
			// 
			this->removeUrlToolStripMenuItem->Name = L"removeUrlToolStripMenuItem";
			this->removeUrlToolStripMenuItem->Size = System::Drawing::Size(135, 22);
			this->removeUrlToolStripMenuItem->Text = L"&Remove Url";
			this->removeUrlToolStripMenuItem->Click += gcnew System::EventHandler(this, &CherryFirewallSettingsPanel::removeUrlToolStripMenuItem_Click);
			// 
			// CherryFirewallSettingsPanel
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->cherryFirewallGroupBox);
			this->Name = L"CherryFirewallSettingsPanel";
			this->Size = System::Drawing::Size(354, 214);
			this->cherryFirewallGroupBox->ResumeLayout(false);
			this->cherryFirewallGroupBox->PerformLayout();
			this->addressListContextMenuStrip->ResumeLayout(false);
			this->ResumeLayout(false);

		}
#pragma endregion
	private: 

		System::Void addAddressButton_Click(System::Object^  sender, System::EventArgs^  e) 
		 {
			EditAddressDialog^ editAddressDialog = gcnew EditAddressDialog();
			DialogResult r = editAddressDialog->ShowDialog(this);

			if(r == DialogResult::OK)
			{
				String^ address = editAddressDialog->addressTextBox->Text;
				addressesListView->Items->Add(address);
				cherryFirewall_->AddAddress(address);
			}
		 }
		
		System::Void editUrlToolStripMenuItem_Click(System::Object^  sender, System::EventArgs^  e) 
		{
			System::String^ oldAddress = addressesListView->SelectedItems[0]->Text;
			EditAddressDialog^ editAddressDialog = gcnew EditAddressDialog();
			editAddressDialog->addressTextBox->Text = oldAddress;
			DialogResult r = editAddressDialog->ShowDialog(this);

			if(r == DialogResult::OK)
			{
				String^ newAddress = editAddressDialog->addressTextBox->Text;
				int index = addressesListView->SelectedIndices[0];
				addressesListView->Items->RemoveAt(index);
				addressesListView->Items->Insert(index, newAddress);
				cherryFirewall_->ChangeAddress(oldAddress, newAddress);
			}
		}

		System::Void removeUrlToolStripMenuItem_Click(System::Object^  sender, System::EventArgs^  e) 
		{
			System::String^ oldAddress = addressesListView->SelectedItems[0]->Text;
			DialogResult r = MessageBox::Show("Are you sure you want to remove this url: " + oldAddress + "?", "Remove url", System::Windows::Forms::MessageBoxButtons::YesNo);
			if(r == DialogResult::Yes)
			{
				int index = addressesListView->SelectedIndices[0];
				addressesListView->Items->RemoveAt(index);
				cherryFirewall_->DeleteAddress(oldAddress);
			}
		}
		
		System::Void addressesListView_MouseClick(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e) 
		{
			if(e->Button == System::Windows::Forms::MouseButtons::Right)
			{
				if(addressesListView->SelectedItems->Count != 0)
					addressListContextMenuStrip->Show(addressesListView, e->Location);
			}
		}
	};
}

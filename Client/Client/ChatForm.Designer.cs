﻿namespace Client
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.enterChat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nicknameData = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.userList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.messageData = new System.Windows.Forms.TextBox();
            this.userMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nameData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // enterChat
            // 
            this.enterChat.Enabled = false;
            this.enterChat.Location = new System.Drawing.Point(584, 5);
            this.enterChat.Margin = new System.Windows.Forms.Padding(4);
            this.enterChat.Name = "enterChat";
            this.enterChat.Size = new System.Drawing.Size(143, 28);
            this.enterChat.TabIndex = 0;
            this.enterChat.Text = "Подключение";
            this.enterChat.UseVisualStyleBackColor = true;
            this.enterChat.Click += new System.EventHandler(this.enterChat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Для подключения к чату введите свое имя: ";
            // 
            // nicknameData
            // 
            this.nicknameData.Enabled = false;
            this.nicknameData.Location = new System.Drawing.Point(307, 6);
            this.nicknameData.Margin = new System.Windows.Forms.Padding(4);
            this.nicknameData.Name = "nicknameData";
            this.nicknameData.Size = new System.Drawing.Size(273, 22);
            this.nicknameData.TabIndex = 2;
            // 
            // chatBox
            // 
            this.chatBox.Enabled = false;
            this.chatBox.Location = new System.Drawing.Point(9, 65);
            this.chatBox.Margin = new System.Windows.Forms.Padding(4);
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.Size = new System.Drawing.Size(500, 333);
            this.chatBox.TabIndex = 3;
            this.chatBox.Text = "";
            // 
            // userList
            // 
            this.userList.Enabled = false;
            this.userList.FormattingEnabled = true;
            this.userList.ItemHeight = 16;
            this.userList.Location = new System.Drawing.Point(519, 65);
            this.userList.Margin = new System.Windows.Forms.Padding(4);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(207, 388);
            this.userList.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(523, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Список пользователей: ";
            // 
            // messageData
            // 
            this.messageData.Enabled = false;
            this.messageData.Location = new System.Drawing.Point(9, 406);
            this.messageData.Margin = new System.Windows.Forms.Padding(4);
            this.messageData.Multiline = true;
            this.messageData.Name = "messageData";
            this.messageData.Size = new System.Drawing.Size(500, 45);
            this.messageData.TabIndex = 7;
            this.messageData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.messageData_KeyUp);
            // 
            // userMenu
            // 
            this.userMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.userMenu.Name = "userMenu";
            this.userMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // nameData
            // 
            this.nameData.AutoSize = true;
            this.nameData.Location = new System.Drawing.Point(59, 31);
            this.nameData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nameData.Name = "nameData";
            this.nameData.Size = new System.Drawing.Size(46, 17);
            this.nameData.TabIndex = 9;
            this.nameData.Text = "label4";
            this.nameData.Visible = false;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 467);
            this.Controls.Add(this.nameData);
            this.Controls.Add(this.messageData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userList);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.nicknameData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.enterChat);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChatForm";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enterChat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nicknameData;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.ListBox userList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox messageData;
        private System.Windows.Forms.ContextMenuStrip userMenu;
        private System.Windows.Forms.Label nameData;
    }
}


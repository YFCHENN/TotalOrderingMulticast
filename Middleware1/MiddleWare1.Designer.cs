namespace MiddleWare1
{
    partial class MiddleWare1
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
            this.sendButton = new System.Windows.Forms.Button();
            this.labelSent = new System.Windows.Forms.Label();
            this.labelReady = new System.Windows.Forms.Label();
            this.labelReceived = new System.Windows.Forms.Label();
            this.listBoxSent = new System.Windows.Forms.ListBox();
            this.listBoxReceived = new System.Windows.Forms.ListBox();
            this.listBoxReady = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendButton.Location = new System.Drawing.Point(57, 76);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(141, 36);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // labelSent
            // 
            this.labelSent.AutoSize = true;
            this.labelSent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSent.Location = new System.Drawing.Point(53, 164);
            this.labelSent.Name = "labelSent";
            this.labelSent.Size = new System.Drawing.Size(47, 22);
            this.labelSent.TabIndex = 1;
            this.labelSent.Text = "Sent";
            // 
            // labelReady
            // 
            this.labelReady.AutoSize = true;
            this.labelReady.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReady.Location = new System.Drawing.Point(820, 164);
            this.labelReady.Name = "labelReady";
            this.labelReady.Size = new System.Drawing.Size(62, 22);
            this.labelReady.TabIndex = 2;
            this.labelReady.Text = "Ready";
            // 
            // labelReceived
            // 
            this.labelReceived.AutoSize = true;
            this.labelReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReceived.Location = new System.Drawing.Point(435, 164);
            this.labelReceived.Name = "labelReceived";
            this.labelReceived.Size = new System.Drawing.Size(85, 22);
            this.labelReceived.TabIndex = 3;
            this.labelReceived.Text = "Received";
            // 
            // listBoxSent
            // 
            this.listBoxSent.FormattingEnabled = true;
            this.listBoxSent.ItemHeight = 20;
            this.listBoxSent.Location = new System.Drawing.Point(57, 226);
            this.listBoxSent.Name = "listBoxSent";
            this.listBoxSent.Size = new System.Drawing.Size(351, 324);
            this.listBoxSent.TabIndex = 4;
            // 
            // listBoxReceived
            // 
            this.listBoxReceived.FormattingEnabled = true;
            this.listBoxReceived.ItemHeight = 20;
            this.listBoxReceived.Location = new System.Drawing.Point(439, 226);
            this.listBoxReceived.Name = "listBoxReceived";
            this.listBoxReceived.Size = new System.Drawing.Size(351, 324);
            this.listBoxReceived.TabIndex = 5;
            // 
            // listBoxReady
            // 
            this.listBoxReady.FormattingEnabled = true;
            this.listBoxReady.ItemHeight = 20;
            this.listBoxReady.Location = new System.Drawing.Point(824, 226);
            this.listBoxReady.Name = "listBoxReady";
            this.listBoxReady.Size = new System.Drawing.Size(351, 324);
            this.listBoxReady.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 671);
            this.Controls.Add(this.listBoxReady);
            this.Controls.Add(this.listBoxReceived);
            this.Controls.Add(this.listBoxSent);
            this.Controls.Add(this.labelReceived);
            this.Controls.Add(this.labelReady);
            this.Controls.Add(this.labelSent);
            this.Controls.Add(this.sendButton);
            this.Name = "Form1";
            this.Text = "Middleware1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label labelSent;
        private System.Windows.Forms.Label labelReady;
        private System.Windows.Forms.Label labelReceived;
        private System.Windows.Forms.ListBox listBoxSent;
        private System.Windows.Forms.ListBox listBoxReceived;
        private System.Windows.Forms.ListBox listBoxReady;
    }
}


namespace WindowsFormsApplication1
{
    partial class MainFrm
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
            this.button1 = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblConnectionID = new System.Windows.Forms.Label();
            this.videoBox = new System.Windows.Forms.PictureBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.skeletonCB = new System.Windows.Forms.CheckBox();
            this.headXlbl = new System.Windows.Forms.Label();
            this.headYlbl = new System.Windows.Forms.Label();
            this.headZlbl = new System.Windows.Forms.Label();
            this.messageTb = new System.Windows.Forms.TextBox();
            this.sendMessageBtn = new System.Windows.Forms.Button();
            this.SVMBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.recorBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.counterLbl = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.fallornahCb = new System.Windows.Forms.CheckBox();
            this.removeBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1003, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send Notification";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(967, 194);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(328, 438);
            this.tbOutput.TabIndex = 2;
            this.tbOutput.Text = "";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(964, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Status";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "ConnectionID";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(161, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(13, 17);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "-";
            // 
            // lblConnectionID
            // 
            this.lblConnectionID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnectionID.AutoSize = true;
            this.lblConnectionID.Location = new System.Drawing.Point(161, 47);
            this.lblConnectionID.Name = "lblConnectionID";
            this.lblConnectionID.Size = new System.Drawing.Size(13, 17);
            this.lblConnectionID.TabIndex = 9;
            this.lblConnectionID.Text = "-";
            // 
            // videoBox
            // 
            this.videoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoBox.Location = new System.Drawing.Point(38, 91);
            this.videoBox.Name = "videoBox";
            this.videoBox.Size = new System.Drawing.Size(920, 541);
            this.videoBox.TabIndex = 10;
            this.videoBox.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(164, 668);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 21);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "rgb";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(327, 668);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(105, 21);
            this.radioButton2.TabIndex = 12;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "DepthImage";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(519, 668);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(58, 21);
            this.radioButton4.TabIndex = 14;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Stop";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // skeletonCB
            // 
            this.skeletonCB.AutoSize = true;
            this.skeletonCB.Location = new System.Drawing.Point(731, 669);
            this.skeletonCB.Name = "skeletonCB";
            this.skeletonCB.Size = new System.Drawing.Size(101, 21);
            this.skeletonCB.TabIndex = 15;
            this.skeletonCB.Text = "skeletonCB";
            this.skeletonCB.UseVisualStyleBackColor = true;
            this.skeletonCB.CheckedChanged += new System.EventHandler(this.skeletonCB_CheckedChanged);
            // 
            // headXlbl
            // 
            this.headXlbl.AutoSize = true;
            this.headXlbl.Location = new System.Drawing.Point(617, 61);
            this.headXlbl.Name = "headXlbl";
            this.headXlbl.Size = new System.Drawing.Size(14, 17);
            this.headXlbl.TabIndex = 16;
            this.headXlbl.Text = "x";
            // 
            // headYlbl
            // 
            this.headYlbl.AutoSize = true;
            this.headYlbl.Location = new System.Drawing.Point(743, 61);
            this.headYlbl.Name = "headYlbl";
            this.headYlbl.Size = new System.Drawing.Size(15, 17);
            this.headYlbl.TabIndex = 17;
            this.headYlbl.Text = "y";
            // 
            // headZlbl
            // 
            this.headZlbl.AutoSize = true;
            this.headZlbl.Location = new System.Drawing.Point(853, 61);
            this.headZlbl.Name = "headZlbl";
            this.headZlbl.Size = new System.Drawing.Size(15, 17);
            this.headZlbl.TabIndex = 18;
            this.headZlbl.Text = "z";
            // 
            // messageTb
            // 
            this.messageTb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTb.Location = new System.Drawing.Point(1003, 13);
            this.messageTb.Name = "messageTb";
            this.messageTb.Size = new System.Drawing.Size(272, 22);
            this.messageTb.TabIndex = 1;
            // 
            // sendMessageBtn
            // 
            this.sendMessageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendMessageBtn.Location = new System.Drawing.Point(1142, 47);
            this.sendMessageBtn.Name = "sendMessageBtn";
            this.sendMessageBtn.Size = new System.Drawing.Size(133, 38);
            this.sendMessageBtn.TabIndex = 19;
            this.sendMessageBtn.Text = "Send Message";
            this.sendMessageBtn.UseVisualStyleBackColor = true;
            this.sendMessageBtn.Click += new System.EventHandler(this.sendMessageBtn_Click);
            // 
            // SVMBtn
            // 
            this.SVMBtn.Location = new System.Drawing.Point(1003, 91);
            this.SVMBtn.Name = "SVMBtn";
            this.SVMBtn.Size = new System.Drawing.Size(133, 35);
            this.SVMBtn.TabIndex = 20;
            this.SVMBtn.Text = "SVM Build Model";
            this.SVMBtn.UseVisualStyleBackColor = true;
            this.SVMBtn.Click += new System.EventHandler(this.SVMBtn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1143, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 35);
            this.button2.TabIndex = 21;
            this.button2.Text = "Classify Input";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // recorBtn
            // 
            this.recorBtn.Location = new System.Drawing.Point(410, 4);
            this.recorBtn.Name = "recorBtn";
            this.recorBtn.Size = new System.Drawing.Size(92, 53);
            this.recorBtn.TabIndex = 22;
            this.recorBtn.Text = "Record";
            this.recorBtn.UseVisualStyleBackColor = true;
            this.recorBtn.Click += new System.EventHandler(this.recorBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // counterLbl
            // 
            this.counterLbl.AutoSize = true;
            this.counterLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.counterLbl.Location = new System.Drawing.Point(524, 9);
            this.counterLbl.Name = "counterLbl";
            this.counterLbl.Size = new System.Drawing.Size(43, 48);
            this.counterLbl.TabIndex = 23;
            this.counterLbl.Text = "5";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(1003, 645);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(116, 44);
            this.saveBtn.TabIndex = 24;
            this.saveBtn.Text = "Save-Csv";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // fallornahCb
            // 
            this.fallornahCb.AutoSize = true;
            this.fallornahCb.Location = new System.Drawing.Point(503, 64);
            this.fallornahCb.Name = "fallornahCb";
            this.fallornahCb.Size = new System.Drawing.Size(94, 21);
            this.fallornahCb.TabIndex = 25;
            this.fallornahCb.Text = "fall/not fall";
            this.fallornahCb.UseVisualStyleBackColor = true;
            // 
            // removeBtn
            // 
            this.removeBtn.Location = new System.Drawing.Point(1142, 645);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(95, 44);
            this.removeBtn.TabIndex = 26;
            this.removeBtn.Text = "Remove last ";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 702);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.fallornahCb);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.counterLbl);
            this.Controls.Add(this.recorBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.SVMBtn);
            this.Controls.Add(this.sendMessageBtn);
            this.Controls.Add(this.headZlbl);
            this.Controls.Add(this.headYlbl);
            this.Controls.Add(this.headXlbl);
            this.Controls.Add(this.skeletonCB);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.videoBox);
            this.Controls.Add(this.lblConnectionID);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.messageTb);
            this.Controls.Add(this.button1);
            this.Name = "MainFrm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox tbOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblConnectionID;
        private System.Windows.Forms.PictureBox videoBox;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.CheckBox skeletonCB;
        private System.Windows.Forms.Label headXlbl;
        private System.Windows.Forms.Label headYlbl;
        private System.Windows.Forms.Label headZlbl;
        private System.Windows.Forms.TextBox messageTb;
        private System.Windows.Forms.Button sendMessageBtn;
        private System.Windows.Forms.Button SVMBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button recorBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label counterLbl;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.CheckBox fallornahCb;
        private System.Windows.Forms.Button removeBtn;
    }
}


namespace WindowsFormsApplication1
{
    partial class Temp
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
            this.videoBox = new System.Windows.Forms.PictureBox();
            this.depthImage = new System.Windows.Forms.PictureBox();
            this.skeletonImage = new System.Windows.Forms.Panel();
            this.colorBtn = new System.Windows.Forms.Button();
            this.depthBtn = new System.Windows.Forms.Button();
            this.skeletonBtn = new System.Windows.Forms.Button();
            this.graphBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.RichTextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblConnectionID = new System.Windows.Forms.Label();
            this.headXlbl = new System.Windows.Forms.Label();
            this.headYlbl = new System.Windows.Forms.Label();
            this.headZlbl = new System.Windows.Forms.Label();
            this.messageTb = new System.Windows.Forms.TextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.updateUIWorker = new System.ComponentModel.BackgroundWorker();
            this.controlButtonGb = new System.Windows.Forms.GroupBox();
            this.testToolsGb = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.kinectInfoGb = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.noPeoplelbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.depthImage)).BeginInit();
            this.controlButtonGb.SuspendLayout();
            this.testToolsGb.SuspendLayout();
            this.kinectInfoGb.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoBox
            // 
            this.videoBox.Location = new System.Drawing.Point(13, 13);
            this.videoBox.Name = "videoBox";
            this.videoBox.Size = new System.Drawing.Size(675, 450);
            this.videoBox.TabIndex = 0;
            this.videoBox.TabStop = false;
            // 
            // depthImage
            // 
            this.depthImage.Location = new System.Drawing.Point(368, 469);
            this.depthImage.Name = "depthImage";
            this.depthImage.Size = new System.Drawing.Size(675, 450);
            this.depthImage.TabIndex = 2;
            this.depthImage.TabStop = false;
            // 
            // skeletonImage
            // 
            this.skeletonImage.Location = new System.Drawing.Point(717, 12);
            this.skeletonImage.Name = "skeletonImage";
            this.skeletonImage.Size = new System.Drawing.Size(675, 450);
            this.skeletonImage.TabIndex = 4;
            this.skeletonImage.Paint += new System.Windows.Forms.PaintEventHandler(this.skeletonImage_Paint);
            // 
            // colorBtn
            // 
            this.colorBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorBtn.Location = new System.Drawing.Point(183, 17);
            this.colorBtn.Name = "colorBtn";
            this.colorBtn.Size = new System.Drawing.Size(200, 60);
            this.colorBtn.TabIndex = 5;
            this.colorBtn.Text = "Color ON";
            this.colorBtn.UseVisualStyleBackColor = true;
            this.colorBtn.Click += new System.EventHandler(this.colorBtn_Click);
            // 
            // depthBtn
            // 
            this.depthBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.depthBtn.Location = new System.Drawing.Point(447, 17);
            this.depthBtn.Name = "depthBtn";
            this.depthBtn.Size = new System.Drawing.Size(200, 60);
            this.depthBtn.TabIndex = 6;
            this.depthBtn.Text = "Depth ON";
            this.depthBtn.UseVisualStyleBackColor = true;
            this.depthBtn.Click += new System.EventHandler(this.depthBtn_Click);
            // 
            // skeletonBtn
            // 
            this.skeletonBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skeletonBtn.Location = new System.Drawing.Point(721, 17);
            this.skeletonBtn.Name = "skeletonBtn";
            this.skeletonBtn.Size = new System.Drawing.Size(200, 60);
            this.skeletonBtn.TabIndex = 7;
            this.skeletonBtn.Text = "Skeleton ON";
            this.skeletonBtn.UseVisualStyleBackColor = true;
            this.skeletonBtn.Click += new System.EventHandler(this.skeletonBtn_Click);
            // 
            // graphBtn
            // 
            this.graphBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphBtn.Location = new System.Drawing.Point(989, 17);
            this.graphBtn.Name = "graphBtn";
            this.graphBtn.Size = new System.Drawing.Size(200, 60);
            this.graphBtn.TabIndex = 8;
            this.graphBtn.Text = "Graph ON";
            this.graphBtn.UseVisualStyleBackColor = true;
            this.graphBtn.Click += new System.EventHandler(this.graphBtn_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(20, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 40);
            this.button1.TabIndex = 9;
            this.button1.Text = "Notify";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(1427, 40);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(343, 522);
            this.tbOutput.TabIndex = 10;
            this.tbOutput.Text = "";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(30, 60);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Status";
            // 
            // lblConnectionID
            // 
            this.lblConnectionID.AutoSize = true;
            this.lblConnectionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionID.Location = new System.Drawing.Point(30, 114);
            this.lblConnectionID.Name = "lblConnectionID";
            this.lblConnectionID.Size = new System.Drawing.Size(92, 17);
            this.lblConnectionID.TabIndex = 12;
            this.lblConnectionID.Text = "ConnectionID";
            // 
            // headXlbl
            // 
            this.headXlbl.AutoSize = true;
            this.headXlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headXlbl.Location = new System.Drawing.Point(132, 169);
            this.headXlbl.Name = "headXlbl";
            this.headXlbl.Size = new System.Drawing.Size(17, 20);
            this.headXlbl.TabIndex = 13;
            this.headXlbl.Text = "x";
            // 
            // headYlbl
            // 
            this.headYlbl.AutoSize = true;
            this.headYlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headYlbl.Location = new System.Drawing.Point(132, 189);
            this.headYlbl.Name = "headYlbl";
            this.headYlbl.Size = new System.Drawing.Size(17, 20);
            this.headYlbl.TabIndex = 14;
            this.headYlbl.Text = "y";
            // 
            // headZlbl
            // 
            this.headZlbl.AutoSize = true;
            this.headZlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headZlbl.Location = new System.Drawing.Point(132, 209);
            this.headZlbl.Name = "headZlbl";
            this.headZlbl.Size = new System.Drawing.Size(18, 20);
            this.headZlbl.TabIndex = 15;
            this.headZlbl.Text = "z";
            // 
            // messageTb
            // 
            this.messageTb.Location = new System.Drawing.Point(20, 58);
            this.messageTb.Name = "messageTb";
            this.messageTb.Size = new System.Drawing.Size(303, 30);
            this.messageTb.TabIndex = 16;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendMessageButton.Location = new System.Drawing.Point(189, 94);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(134, 43);
            this.sendMessageButton.TabIndex = 17;
            this.sendMessageButton.Text = "Message";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1562, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Output";
            // 
            // updateUIWorker
            // 
            this.updateUIWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.updateUIWorker_DoWork);
            // 
            // controlButtonGb
            // 
            this.controlButtonGb.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.controlButtonGb.Controls.Add(this.skeletonBtn);
            this.controlButtonGb.Controls.Add(this.colorBtn);
            this.controlButtonGb.Controls.Add(this.depthBtn);
            this.controlButtonGb.Controls.Add(this.graphBtn);
            this.controlButtonGb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlButtonGb.Location = new System.Drawing.Point(13, 934);
            this.controlButtonGb.Name = "controlButtonGb";
            this.controlButtonGb.Size = new System.Drawing.Size(1379, 91);
            this.controlButtonGb.TabIndex = 19;
            this.controlButtonGb.TabStop = false;
            this.controlButtonGb.Text = "Control Buttons";
            // 
            // testToolsGb
            // 
            this.testToolsGb.Controls.Add(this.label6);
            this.testToolsGb.Controls.Add(this.label5);
            this.testToolsGb.Controls.Add(this.label4);
            this.testToolsGb.Controls.Add(this.messageTb);
            this.testToolsGb.Controls.Add(this.sendMessageButton);
            this.testToolsGb.Controls.Add(this.headYlbl);
            this.testToolsGb.Controls.Add(this.button1);
            this.testToolsGb.Controls.Add(this.headXlbl);
            this.testToolsGb.Controls.Add(this.headZlbl);
            this.testToolsGb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testToolsGb.Location = new System.Drawing.Point(1427, 597);
            this.testToolsGb.Name = "testToolsGb";
            this.testToolsGb.Size = new System.Drawing.Size(343, 248);
            this.testToolsGb.TabIndex = 20;
            this.testToolsGb.TabStop = false;
            this.testToolsGb.Text = "Test tools";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "HeadZ:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "HeadY:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "HeadX:";
            // 
            // kinectInfoGb
            // 
            this.kinectInfoGb.Controls.Add(this.noPeoplelbl);
            this.kinectInfoGb.Controls.Add(this.label7);
            this.kinectInfoGb.Controls.Add(this.label3);
            this.kinectInfoGb.Controls.Add(this.label2);
            this.kinectInfoGb.Controls.Add(this.lblStatus);
            this.kinectInfoGb.Controls.Add(this.lblConnectionID);
            this.kinectInfoGb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kinectInfoGb.Location = new System.Drawing.Point(1427, 851);
            this.kinectInfoGb.Name = "kinectInfoGb";
            this.kinectInfoGb.Size = new System.Drawing.Size(343, 174);
            this.kinectInfoGb.TabIndex = 21;
            this.kinectInfoGb.TabStop = false;
            this.kinectInfoGb.Text = "Kinect Info";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "ConnectionID: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Status:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "No . tracked People: ";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // noPeoplelbl
            // 
            this.noPeoplelbl.AutoSize = true;
            this.noPeoplelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noPeoplelbl.Location = new System.Drawing.Point(257, 140);
            this.noPeoplelbl.Name = "noPeoplelbl";
            this.noPeoplelbl.Size = new System.Drawing.Size(16, 17);
            this.noPeoplelbl.TabIndex = 16;
            this.noPeoplelbl.Text = "0";
            // 
            // Temp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 1037);
            this.Controls.Add(this.kinectInfoGb);
            this.Controls.Add(this.testToolsGb);
            this.Controls.Add(this.controlButtonGb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.skeletonImage);
            this.Controls.Add(this.depthImage);
            this.Controls.Add(this.videoBox);
            this.Name = "Temp";
            this.Text = "Temp";
            this.Load += new System.EventHandler(this.Temp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.depthImage)).EndInit();
            this.controlButtonGb.ResumeLayout(false);
            this.testToolsGb.ResumeLayout(false);
            this.testToolsGb.PerformLayout();
            this.kinectInfoGb.ResumeLayout(false);
            this.kinectInfoGb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox videoBox;
        private System.Windows.Forms.PictureBox depthImage;
        private System.Windows.Forms.Panel skeletonImage;
        private System.Windows.Forms.Button colorBtn;
        private System.Windows.Forms.Button depthBtn;
        private System.Windows.Forms.Button skeletonBtn;
        private System.Windows.Forms.Button graphBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox tbOutput;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblConnectionID;
        private System.Windows.Forms.Label headXlbl;
        private System.Windows.Forms.Label headYlbl;
        private System.Windows.Forms.Label headZlbl;
        private System.Windows.Forms.TextBox messageTb;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker updateUIWorker;
        private System.Windows.Forms.GroupBox controlButtonGb;
        private System.Windows.Forms.GroupBox testToolsGb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox kinectInfoGb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label noPeoplelbl;
    }
}
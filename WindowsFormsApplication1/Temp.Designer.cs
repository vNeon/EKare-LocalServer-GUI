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
            this.graphImage = new System.Windows.Forms.PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.depthImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphImage)).BeginInit();
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
            this.depthImage.Location = new System.Drawing.Point(13, 478);
            this.depthImage.Name = "depthImage";
            this.depthImage.Size = new System.Drawing.Size(675, 450);
            this.depthImage.TabIndex = 2;
            this.depthImage.TabStop = false;
            // 
            // graphImage
            // 
            this.graphImage.Location = new System.Drawing.Point(717, 478);
            this.graphImage.Name = "graphImage";
            this.graphImage.Size = new System.Drawing.Size(675, 450);
            this.graphImage.TabIndex = 3;
            this.graphImage.TabStop = false;
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
            this.colorBtn.Location = new System.Drawing.Point(52, 966);
            this.colorBtn.Name = "colorBtn";
            this.colorBtn.Size = new System.Drawing.Size(166, 59);
            this.colorBtn.TabIndex = 5;
            this.colorBtn.Text = "Color ON";
            this.colorBtn.UseVisualStyleBackColor = true;
            this.colorBtn.Click += new System.EventHandler(this.colorBtn_Click);
            // 
            // depthBtn
            // 
            this.depthBtn.Location = new System.Drawing.Point(266, 966);
            this.depthBtn.Name = "depthBtn";
            this.depthBtn.Size = new System.Drawing.Size(166, 59);
            this.depthBtn.TabIndex = 6;
            this.depthBtn.Text = "Depth ON";
            this.depthBtn.UseVisualStyleBackColor = true;
            this.depthBtn.Click += new System.EventHandler(this.depthBtn_Click);
            // 
            // skeletonBtn
            // 
            this.skeletonBtn.Location = new System.Drawing.Point(471, 966);
            this.skeletonBtn.Name = "skeletonBtn";
            this.skeletonBtn.Size = new System.Drawing.Size(166, 59);
            this.skeletonBtn.TabIndex = 7;
            this.skeletonBtn.Text = "Skeleton ON";
            this.skeletonBtn.UseVisualStyleBackColor = true;
            this.skeletonBtn.Click += new System.EventHandler(this.skeletonBtn_Click);
            // 
            // graphBtn
            // 
            this.graphBtn.Location = new System.Drawing.Point(695, 966);
            this.graphBtn.Name = "graphBtn";
            this.graphBtn.Size = new System.Drawing.Size(166, 59);
            this.graphBtn.TabIndex = 8;
            this.graphBtn.Text = "Graph ON";
            this.graphBtn.UseVisualStyleBackColor = true;
            this.graphBtn.Click += new System.EventHandler(this.graphBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1427, 919);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 40);
            this.button1.TabIndex = 9;
            this.button1.Text = "Send notification";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(1427, 82);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(343, 779);
            this.tbOutput.TabIndex = 10;
            this.tbOutput.Text = "";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(1507, 976);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Status";
            // 
            // lblConnectionID
            // 
            this.lblConnectionID.AutoSize = true;
            this.lblConnectionID.Location = new System.Drawing.Point(1507, 1004);
            this.lblConnectionID.Name = "lblConnectionID";
            this.lblConnectionID.Size = new System.Drawing.Size(92, 17);
            this.lblConnectionID.TabIndex = 12;
            this.lblConnectionID.Text = "ConnectionID";
            // 
            // headXlbl
            // 
            this.headXlbl.AutoSize = true;
            this.headXlbl.Location = new System.Drawing.Point(1169, 954);
            this.headXlbl.Name = "headXlbl";
            this.headXlbl.Size = new System.Drawing.Size(14, 17);
            this.headXlbl.TabIndex = 13;
            this.headXlbl.Text = "x";
            // 
            // headYlbl
            // 
            this.headYlbl.AutoSize = true;
            this.headYlbl.Location = new System.Drawing.Point(1169, 976);
            this.headYlbl.Name = "headYlbl";
            this.headYlbl.Size = new System.Drawing.Size(17, 17);
            this.headYlbl.TabIndex = 14;
            this.headYlbl.Text = "Y";
            // 
            // headZlbl
            // 
            this.headZlbl.AutoSize = true;
            this.headZlbl.Location = new System.Drawing.Point(1169, 993);
            this.headZlbl.Name = "headZlbl";
            this.headZlbl.Size = new System.Drawing.Size(17, 17);
            this.headZlbl.TabIndex = 15;
            this.headZlbl.Text = "Z";
            // 
            // messageTb
            // 
            this.messageTb.Location = new System.Drawing.Point(1427, 880);
            this.messageTb.Name = "messageTb";
            this.messageTb.Size = new System.Drawing.Size(343, 22);
            this.messageTb.TabIndex = 16;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(1636, 919);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(134, 40);
            this.sendMessageButton.TabIndex = 17;
            this.sendMessageButton.Text = "Send Message";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1424, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Output";
            // 
            // updateUIWorker
            // 
            this.updateUIWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.updateUIWorker_DoWork);
            // 
            // Temp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 1037);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.messageTb);
            this.Controls.Add(this.headZlbl);
            this.Controls.Add(this.headYlbl);
            this.Controls.Add(this.headXlbl);
            this.Controls.Add(this.lblConnectionID);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.graphBtn);
            this.Controls.Add(this.skeletonBtn);
            this.Controls.Add(this.depthBtn);
            this.Controls.Add(this.colorBtn);
            this.Controls.Add(this.skeletonImage);
            this.Controls.Add(this.graphImage);
            this.Controls.Add(this.depthImage);
            this.Controls.Add(this.videoBox);
            this.Name = "Temp";
            this.Text = "Temp";
            this.Load += new System.EventHandler(this.Temp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.depthImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox videoBox;
        private System.Windows.Forms.PictureBox depthImage;
        private System.Windows.Forms.PictureBox graphImage;
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
    }
}
﻿namespace DataLogger
{
    partial class GetValueToSaveData
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.days = new System.Windows.Forms.TextBox();
            this.hours = new System.Windows.Forms.TextBox();
            this.minutes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.seconds = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(321, 16);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(38, 20);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // days
            // 
            this.days.Location = new System.Drawing.Point(22, 16);
            this.days.Name = "days";
            this.days.Size = new System.Drawing.Size(38, 20);
            this.days.TabIndex = 0;
            this.days.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.days_KeyPress);
            // 
            // hours
            // 
            this.hours.Location = new System.Drawing.Point(99, 16);
            this.hours.Name = "hours";
            this.hours.Size = new System.Drawing.Size(38, 20);
            this.hours.TabIndex = 1;
            this.hours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hours_KeyPress);
            // 
            // minutes
            // 
            this.minutes.Location = new System.Drawing.Point(165, 16);
            this.minutes.Name = "minutes";
            this.minutes.Size = new System.Drawing.Size(38, 20);
            this.minutes.TabIndex = 2;
            this.minutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.minutes_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ngày";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "giờ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "phút";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(282, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "giây";
            // 
            // seconds
            // 
            this.seconds.Location = new System.Drawing.Point(241, 16);
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(38, 20);
            this.seconds.TabIndex = 7;
            this.seconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seconds_KeyPress);
            // 
            // GetValueToSaveData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 50);
            this.Controls.Add(this.seconds);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minutes);
            this.Controls.Add(this.hours);
            this.Controls.Add(this.days);
            this.Controls.Add(this.btn_OK);
            this.Name = "GetValueToSaveData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thời gian lưu dữ liệu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetValueToSaveData_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GetValueToSaveData_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.TextBox days;
        private System.Windows.Forms.TextBox hours;
        private System.Windows.Forms.TextBox minutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox seconds;
    }
}
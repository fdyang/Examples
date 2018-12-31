﻿using System.Diagnostics;

namespace KingProService
{
    partial class KingProService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Diagnostics.EventLog eventLog;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.eventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).BeginInit();
            // 
            // eventLog
            // 
            this.eventLog.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.eventLog_EntryWritten);
            // 

            components = new System.ComponentModel.Container();
            this.ServiceName = "KingProService";

            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).EndInit();
        }

                /// <summary>
        /// The eventLog_EntryWritten handle.
        /// </summary>
        /// <param name="sender">The object of sender.</param>
        /// <param name="e">The EntryWrittenEventArgs.</param>
        private void eventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
        }


        #endregion
    }
}

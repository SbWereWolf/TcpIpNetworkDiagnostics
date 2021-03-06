﻿using System;
using System.Windows.Forms;

namespace RuskomDiagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ShowMessageForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public string FormTitle { private get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        public string FormMessage { private get ; set ; }

        /// <summary>
        /// </summary>
        private bool MessageShown { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        public ShowMessageForm ( )
        {
            this.InitializeComponent( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCopyResultButton_Click
            (
            object sender ,
            EventArgs e )
        {
            this.CopyResult( ) ;
        }

        /// <summary>
        /// </summary>
        private void CopyResult ( )
        {
            var formMessageTextBox = this.FormMessageTextBox ;
            var overallString = formMessageTextBox?.Text ;
            if ( overallString != null )
            {
                Clipboard.SetText ( overallString ) ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCloseButton_Click
            (
            object sender ,
            EventArgs e )
        {
            Handler.CloseForm ( this ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageForm_Load
            (
            object sender ,
            EventArgs e )
        {
            this.MessageShown = false ;
            Handler.SetFormPositionsOnScreenCenter ( this ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageForm_Shown
            (
            object sender ,
            EventArgs e )
        {
            if ( this.MessageShown )
            {
                return ;
            }
            this.MessageShown = true ;
            this.Text = this.FormTitle ;
            var formMessageTextBox = this.FormMessageTextBox ;
            if ( formMessageTextBox != null )
            {
                formMessageTextBox.Text = this.FormMessage ;
            }
        }
    }
}

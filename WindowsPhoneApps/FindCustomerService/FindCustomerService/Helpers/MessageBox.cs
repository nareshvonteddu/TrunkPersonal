using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FindCustomerService.Helpers
{
    public sealed class MessageBox
    {
        // Abstract: 
        //     The display contains the specified text and the "set" button of the message box. 
        //
        // Parameters: 
        //   messageBoxText:
        //     Message to display. 
        //
        // Abnormal: 
        //   System.ArgumentNullException:
        //     MessageBoxText null. 
        public async static Task<MessageBoxResult> Show(string messageBoxText)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException("messageBoxText is null");

            var tcs = new TaskCompletionSource<MessageBoxResult>();

            var dialog = new MessageDialog(messageBoxText);

            dialog.Commands.Add(new UICommand("Ok", command =>
            {
                tcs.SetResult((MessageBoxResult)command.Id);
            }, MessageBoxResult.OK));

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 0;

            await dialog.ShowAsync();

            return await tcs.Task;
        }

        //
        // Abstract: 
        //     The display contains the specified text, the title bar caption button and response message box. 
        //
        // Parameters: 
        //   messageBoxText:
        //     Message to display. 
        //
        //   caption:
        //     The message box title. 
        //
        //   button:
        //     A value used to indicate which button, or which buttons to display. 
        //
        // Return result: 
        //     A value used to indicate to the user, in response to the news. 
        //
        // Abnormal: 
        //   System.ArgumentNullException:
        //     MessageBoxText null. - or -caption null. 
        //
        //   System.ArgumentException:
        //     Button is not a valid System.Windows.MessageBoxButton value. 
        public async static Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException("messageBoxText is null");
            if (caption == null)
                throw new ArgumentNullException("caption is null");
            if (button != MessageBoxButton.OK && button != MessageBoxButton.OKCancel)
                throw new ArgumentException("button is null");

            var tcs = new TaskCompletionSource<MessageBoxResult>();

            var dialog = new MessageDialog(messageBoxText, caption);

            dialog.Commands.Add(new UICommand("Ok", command =>
            {
                tcs.SetResult((MessageBoxResult)command.Id);
            }, MessageBoxResult.OK));

            if (button == MessageBoxButton.OKCancel)
            {
                dialog.Commands.Add(new UICommand("Cancel", command =>
                {
                    tcs.SetResult((MessageBoxResult)command.Id);
                }, MessageBoxResult.Cancel));
            }

            dialog.DefaultCommandIndex = 0;
            if (button == MessageBoxButton.OKCancel)
                dialog.CancelCommandIndex = 1;
            else
                dialog.CancelCommandIndex = 0;

            await dialog.ShowAsync();

            return await tcs.Task;
        }
    }

    // Abstract: 
    //     Specifies the display message boxes to contain buttons. 
    public enum MessageBoxButton
    {
        // Abstract: 
        //     Show only the "set" button. 
        OK = 0,
        //
        // Abstract: 
        //     At the same time display "OK" and "Cancel" button. 
        OKCancel = 1,
    }

    // Abstract: 
    //     Said the message box user response. 
    public enum MessageBoxResult
    {
        // Abstract: 
        //     This value is not currently in use. 
        None = 0,
        //
        // Abstract: 
        //     The user clicks the "set" button. 
        OK = 1,
        //
        // Abstract: 
        //     The user clicks the "Cancel" button or press the ESC. 
        Cancel = 2,
        //
        // Abstract: 
        //     This value is not currently in use. 
        Yes = 6,
        //
        // Abstract: 
        //     This value is not currently in use. 
        No = 7,
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiFaceRec.View
{
    public partial class CardReader : Form
    {
        public CardReader()
        {
            InitializeComponent();

            var cts = new CancellationTokenSource();

            RT(100, cts.Token);

            Console.ReadLine();

            cts.Cancel();

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void RT(int milliseconds, CancellationToken token)
        {
            new Thread(() =>
            {
                int port = 0;
                bool isSusscess = true;
            connect_usb:
                isSusscess = MLDeviceControler.CN670.OpenUSBport(port);
                if (isSusscess)
                {
                    String lastCardId = "";
                    while (token.IsCancellationRequested)
                    {
                        try
                        {
                            String buffer = MLDeviceControler.CN670.ReadCardID(port);
                            if (buffer != null && !buffer.Equals(lastCardId))
                            {
                                lastCardId = buffer;
                                if (!lastCardId.Equals(""))
                                {
                                    textBox1.Text = buffer;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MLDeviceControler.CN670.CloseUSBport();
                            goto connect_usb;
                        }
                    }

                    isSusscess = MLDeviceControler.CN670.CloseUSBport();
                    if (!isSusscess)
                    {
                    }
                    else
                    {
                    }
                }
                else
                {
                    goto connect_usb;
                }
            })
            { IsBackground = true }.Start();
        }
    }
}

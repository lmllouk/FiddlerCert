﻿using System;
using System.Windows.Forms;
using Fiddler;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace VCSJones.FiddlerCert
{
    public class CertificateInspector : IFiddlerExtension
    {
        private bool _isSupportedOperatingSystem = false;
        internal static readonly Dictionary<Tuple<string, int>, Tuple<X509Chain, X509Certificate2>> ServerCertificates = new Dictionary<Tuple<string, int>, Tuple<X509Chain, X509Certificate2>>();

        private void CertificateValidationHandler(object sender, ValidateServerCertificateEventArgs e)
        {
            lock (ServerCertificates)
            {
                var key = Tuple.Create(e.Session.hostname, e.Session.port);
                if (!ServerCertificates.ContainsKey(key))
                {
                    ServerCertificates.Add(key, Tuple.Create(new X509Chain(e.ServerCertificateChain.ChainContext), new X509Certificate2(e.ServerCertificate)));
                }
            }
        }

        public void OnLoad()
        {
            _isSupportedOperatingSystem = Environment.OSVersion.Version >= new Version(6, 0);
            if (!_isSupportedOperatingSystem)
            {
                MessageBox.Show("Windows Vista / Server 2008 or greater is required for the Certificate inspector extension to function.");
            }
            else if (!CONFIG.bCaptureCONNECT || !CONFIG.bMITM_HTTPS)
            {
                MessageBox.Show("FiddlerCert will not work without capturing HTTPS CONNECTs. Please enable CONNECT capturing in configuration.");
            }
            else
            {
                FiddlerApplication.OnValidateServerCertificate += CertificateValidationHandler;
            }
        }

        public void OnBeforeUnload()
        {
        }
    }

}


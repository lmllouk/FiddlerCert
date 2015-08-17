﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VCSJones.FiddlerCert
{
    public class CertificateModel : INotifyPropertyChanged
    {
        private string _commonName;
        private string _thumbprint;
        private string _subjectAlternativeName;
        private PublicKeyModel _publicKey;
        private DateTime _beginDate;
        private DateTime _endDate;
        private SignatureAlgorithmModel _signatureAlgorithm;
        private AsyncProperty<CertificateErrors> _errors;
        private RelayCommand _viewCommand, _installCommand;
        private AsyncProperty<SpkiHashesModel> _spkiHashes;

        public string CommonName
        {
            get
            {
                return _commonName;
            }
            set
            {
                _commonName = value;
                OnPropertyChanged();
            }
        }

        public string Thumbprint
        {
            get
            {
                return _thumbprint;
            }
            set
            {
                _thumbprint = value;
                OnPropertyChanged();
            }
        }

        public string SubjectAlternativeName
        {
            get
            {
                return _subjectAlternativeName;
            }
            set
            {
                _subjectAlternativeName = value;
                OnPropertyChanged();
            }
        }

        public PublicKeyModel PublicKey
        {
            get { return _publicKey; }
            set
            {
                _publicKey = value;
                OnPropertyChanged();
            }
        }

        public DateTime BeginDate
        {
            get { return _beginDate; }
            set
            {
                _beginDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged();
                // ReSharper disable once ExplicitCallerInfoArgument
                OnPropertyChanged(nameof(ExpiresIn));
            }
        }

        public SignatureAlgorithmModel SignatureAlgorithm
        {
            get { return _signatureAlgorithm; }
            set
            {
                _signatureAlgorithm = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan ExpiresIn => EndDate - DateTime.Now;

        public AsyncProperty<CertificateErrors> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                OnPropertyChanged();
            }
        }

        public AsyncProperty<SpkiHashesModel> SpkiHashes
        {
            get { return _spkiHashes; }
            set
            {
                _spkiHashes = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ViewCommand
        {
            get { return _viewCommand; }
            set
            {
                _viewCommand = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand InstallCommand
        {
            get { return _installCommand; }
            set
            {
                _installCommand = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
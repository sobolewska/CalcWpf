using System.ComponentModel;
using System.Windows.Input;
using CalcWpf.Models;
using CalcWpf.Enums;
using System.Windows;

namespace CalcWpf.ViewModels
{
    class CalcViewModel : INotifyPropertyChanged
    {

        public CalcViewModel()
        {
            _calcModel = new CalcModel();
        }

        CalcModel _calcModel;

        public CalcModel CalcModel
        {
            get { return _calcModel; }
            private set { _calcModel = value; }
        }

        private string[] _numbers = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public string[] Numbers
        {
            get { return _numbers; }
        }

        public string Op_add { get { return EnumData.GetEnumDescription(EOperators.op_add); } }
        public string Op_sub { get { return EnumData.GetEnumDescription(EOperators.op_sub); } }
        public string Op_div { get { return EnumData.GetEnumDescription(EOperators.op_div); } }
        public string Op_mul { get { return EnumData.GetEnumDescription(EOperators.op_mul); } }
        public string Op_pow_2 { get { return EnumData.GetEnumDescription(EOperators.op_pow_2); } }
        public string Op_pow_x { get { return EnumData.GetEnumDescription(EOperators.op_pow_x); } }
        public string Op_root_2 { get { return EnumData.GetEnumDescription(EOperators.op_root_2); } }
        public string Op_root_x { get { return EnumData.GetEnumDescription(EOperators.op_root_x); } }
        public string Op_res { get { return EnumData.GetEnumDescription(EOperators.op_res); } }
        public string Op_clear { get { return EnumData.GetEnumDescription(EOperators.op_clear); } }
        public string Op_comma { get { return EnumData.GetEnumDescription(EOperators.op_comma); } }
        public string Op_back { get { return EnumData.GetEnumDescription(EOperators.op_back); } }
        public string Op_negative { get { return EnumData.GetEnumDescription(EOperators.op_negative); } }

        private string _userInputDispaly = "";

        public string UserInputDispaly
        {
            get { return _userInputDispaly; }
            set
            {
                if (!string.Equals(_userInputDispaly, value))
                {
                    _userInputDispaly = value;
                    RaisePropertyChanged("UserInputDispaly");
                }

            }
        }

        private string _resultDisplay = "";

        public string ResultDisplay
        {
            get { return _resultDisplay; }
            set
            {
                if (!string.Equals(_resultDisplay, value))
                {
                    _resultDisplay = value;
                    RaisePropertyChanged("ResultDisplay");
                }
            }
        }

        void UpdateResultExecute()
        {
            ResultDisplay = CalcModel.calcRes(_userInputDispaly);
        }

        bool CanUpdateResultExecute()
        {
            return true;
        }

        public ICommand UpdateResult { get { return new Commands.RelayCommand(UpdateResultExecute, CanUpdateResultExecute); } }

        void ClearExecute()
        {
            UserInputDispaly = "";
            ResultDisplay = "";

        }

        bool CanClearExecute()
        {
            return true;
        }

        public ICommand Clear { get { return new Commands.RelayCommand(ClearExecute, CanClearExecute); } }

        void BackspaceExecute()
        {
            string[] ops = { Op_add, Op_sub, Op_div, Op_mul, Op_pow_2, Op_pow_x, Op_root_2, Op_root_x, Op_negative };

            // if (ops.Any(x => _userInput.EndsWith(x)))
            if (_userInputDispaly.Length > 0)
            {
                for (int i = 0; i < ops.Length; i++)
                {
                    if (_userInputDispaly.EndsWith(ops[i]))
                    {
                        UserInputDispaly = _userInputDispaly.Remove(_userInputDispaly.Length - ops[i].Length);
                        return;
                    }
                }
                UserInputDispaly = _userInputDispaly.Remove(_userInputDispaly.Length - 1);
            }
        }

        bool CanBackspaceExecute()
        {
            return true;
        }

        public ICommand Backspace { get { return new Commands.RelayCommand(BackspaceExecute, CanBackspaceExecute); } }

        void PressButtonExecute(string parameter)
        {
            UserInputDispaly += parameter;
        }

        bool CanPressButtonExecute(string parameter)
        {
            return true;
        }

        public ICommand PressButton { get { return new Commands.RelayCommand<string>(PressButtonExecute, CanPressButtonExecute); } }

        void ExitCommandExecute()
        {
            Application.Current.Shutdown();
        }

        bool CanExitCommandExecute()
        {
            return true;
        }

        public ICommand ExitCommand { get { return new Commands.RelayCommand(ExitCommandExecute, CanExitCommandExecute); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

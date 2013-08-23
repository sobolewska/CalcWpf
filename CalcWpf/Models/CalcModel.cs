using System;
using CalcWpf.Enums;

namespace CalcWpf.Models
{
    class CalcModel
    {

        private string _userinput = "";

        public string calcRes(string input)
        {
            _userinput = input;
            if (CheckInput())
            {
                return DoCalc();
            }
            else
            {
                return "Error!";
            }
        }

        private bool CheckInput()
        {
            _userinput = _userinput.Replace(EnumData.GetEnumDescription(EOperators.op_mul), ";" + EnumData.GetEnumDescription(EOperators.op_mul) + ";");
            _userinput = _userinput.Replace(EnumData.GetEnumDescription(EOperators.op_div), ";" + EnumData.GetEnumDescription(EOperators.op_div) + ";");
            _userinput = _userinput.Replace(EnumData.GetEnumDescription(EOperators.op_add), ";" + EnumData.GetEnumDescription(EOperators.op_add) + ";");
            _userinput = _userinput.Replace(EnumData.GetEnumDescription(EOperators.op_sub), ";" + EnumData.GetEnumDescription(EOperators.op_sub) + ";");
            _userinput = _userinput.Replace(EnumData.GetEnumDescription(EOperators.op_pow_2), ";" + EnumData.GetEnumDescription(EOperators.op_pow_x) + ";2;");
            _userinput = _userinput.Replace(EnumData.GetEnumDescription(EOperators.op_pow_x), ";" + EnumData.GetEnumDescription(EOperators.op_pow_x) + ";");
            _userinput = _userinput.Replace(EnumData.GetEnumDescription(EOperators.op_root_2), ";2;" + EnumData.GetEnumDescription(EOperators.op_root_x) + ";");
            _userinput = _userinput.Replace(EnumData.GetEnumDescription(EOperators.op_root_x), ";" + EnumData.GetEnumDescription(EOperators.op_root_x) + ";");

            _userinput = _userinput.Replace(";;", ";");

            if (_userinput.StartsWith(";"))
            {
                _userinput = _userinput.Remove(0, 1);
            }
            if (_userinput.EndsWith(";"))
            {
                _userinput = _userinput.Remove(_userinput.Length - 1);
            }

            string[] inputSplit = _userinput.Split(';');

            if (inputSplit.Length == 1 && !IsNumber(inputSplit[0]))
            {
                return false;
            }
            else if (inputSplit.Length % 2 == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < inputSplit.Length - 2; i += 2)
                {
                    if (!IsNumber(inputSplit[i]))
                    {
                        return false;
                    }
                    if (IsNumber(inputSplit[i + 1]))
                    {
                        return false;
                    }
                    if (!IsNumber(inputSplit[i + 2]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsNumber(string input)
        {
            double n;
            return double.TryParse(input.Replace(EnumData.GetEnumDescription(EOperators.op_negative), "-"), out n);
        }

        private string DoCalc()
        {
            string op = "";
            string atomicOp = "";

            while (!IsNumber(_userinput))
            {

                #region nth root exponentiation and
                // if contains both then check which is first from left 
                if (_userinput.Contains(EnumData.GetEnumDescription(EOperators.op_root_x)) && _userinput.Contains(EnumData.GetEnumDescription(EOperators.op_pow_x)))
                {
                    if (_userinput.IndexOf(EnumData.GetEnumDescription(EOperators.op_root_x)) < _userinput.IndexOf(EnumData.GetEnumDescription(EOperators.op_pow_x)))
                    {
                        op = EnumData.GetEnumDescription(EOperators.op_root_x);
                    }
                    else
                    {
                        op = EnumData.GetEnumDescription(EOperators.op_pow_x);
                    }
                }
                // if contains only nth root
                else if (_userinput.Contains(EnumData.GetEnumDescription(EOperators.op_root_x)))
                {
                    op = EnumData.GetEnumDescription(EOperators.op_root_x);
                }
                // if contains only exponentiation
                else if (_userinput.Contains(EnumData.GetEnumDescription(EOperators.op_pow_x)))
                {
                    op = EnumData.GetEnumDescription(EOperators.op_pow_x);
                }
                #endregion
                #region multiplication and division
                // if contains both then check which is first from left 
                else if (_userinput.Contains(EnumData.GetEnumDescription(EOperators.op_mul)) && _userinput.Contains(EnumData.GetEnumDescription(EOperators.op_div)))
                {
                    if (_userinput.IndexOf(EnumData.GetEnumDescription(EOperators.op_mul)) < _userinput.IndexOf(EnumData.GetEnumDescription(EOperators.op_div)))
                    {
                        op = EnumData.GetEnumDescription(EOperators.op_mul);
                    }
                    else
                    {
                        op = EnumData.GetEnumDescription(EOperators.op_div);
                    }
                }
                // if contains only multiplication
                else if (_userinput.Contains(EnumData.GetEnumDescription(EOperators.op_mul)))
                {
                    op = EnumData.GetEnumDescription(EOperators.op_mul);
                }
                // if contains only division
                else if (_userinput.Contains(EnumData.GetEnumDescription(EOperators.op_div)))
                {
                    op = EnumData.GetEnumDescription(EOperators.op_div);
                }
                #endregion
                #region addition and subtraction
                // if contains both then check which is first from left 
                else if (_userinput.Contains(EnumData.GetEnumDescription(EOperators.op_add)) && _userinput.Contains(EnumData.GetEnumDescription(EOperators.op_sub)))
                {
                    if (_userinput.IndexOf(EnumData.GetEnumDescription(EOperators.op_add)) < _userinput.IndexOf(EnumData.GetEnumDescription(EOperators.op_sub)))
                    {
                        op = EnumData.GetEnumDescription(EOperators.op_add);
                    }
                    else
                    {
                        op = EnumData.GetEnumDescription(EOperators.op_sub);
                    }
                }
                // if contains only addition
                else if (_userinput.Contains(EnumData.GetEnumDescription(EOperators.op_add)))
                {
                    op = EnumData.GetEnumDescription(EOperators.op_add);
                }
                // if contains only subtraction
                else if (_userinput.Contains(EnumData.GetEnumDescription(EOperators.op_sub)))
                {
                    op = EnumData.GetEnumDescription(EOperators.op_sub);
                }
                #endregion

                atomicOp = TakeAtomocEq(_userinput, op);
                _userinput = _userinput.Replace(atomicOp, DoCalcAtomic(atomicOp).ToString());
            }

            return _userinput;
        }

        private string TakeAtomocEq(string input, string op)
        {
            input = ";" + input + ";";
            int startpos = input.LastIndexOf(';', input.IndexOf(op) - 2) + 1;
            int endpos = input.IndexOf(';', input.IndexOf(op) + op.Length + 1);
            return input.Substring(startpos, (endpos - startpos));
        }

        private double DoCalcAtomic(string atomocEq)
        {
            double left = Convert.ToDouble(atomocEq.Split(';')[0].Replace(EnumData.GetEnumDescription(EOperators.op_negative), "-"));
            double right = Convert.ToDouble(atomocEq.Split(';')[2].Replace(EnumData.GetEnumDescription(EOperators.op_negative), "-"));
            string op = atomocEq.Split(';')[1];

            if (op.Equals(EnumData.GetEnumDescription(EOperators.op_add)))
            {
                return (left + right);
            }
            else if (op.Equals(EnumData.GetEnumDescription(EOperators.op_sub)))
            {
                return (left - right);
            }
            else if (op.Equals(EnumData.GetEnumDescription(EOperators.op_mul)))
            {
                return (left * right);
            }
            else if (op.Equals(EnumData.GetEnumDescription(EOperators.op_div)))
            {
                return (left / right);
            }
            else if (op.Equals(EnumData.GetEnumDescription(EOperators.op_pow_x)))
            {
                return (Math.Pow(left, right));
            }
            else if (op.Equals(EnumData.GetEnumDescription(EOperators.op_root_x)))
            {
                return (Math.Pow(right, 1.0 / left));
            }
            else
            {
                return 0;
            }
        }
    }
}

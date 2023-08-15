using LamConference.HandlerModels;
using LamConference.ViewModel;
using System;
using System.Text.RegularExpressions;

namespace LamConference.Handlers{
    public class ModelHandler{

        private bool StringCheck(StringHandlerModel model)
        {
            if(model != null)
            {
                var letters = @"^[A-Za-z]+$";
                if(Regex.IsMatch(model.FirstName, letters, RegexOptions.IgnoreCase) 
                && Regex.IsMatch(model.LastName, letters, RegexOptions.IgnoreCase))
                {
                    return true;
                }

                return false;
            }
            return false;
        }

        private bool EnumCheck(EnumHandlerModel model)
        {
            if(model != null)
            {
                if(Enum.IsDefined(model.Level) && Enum.IsDefined(model.Level))
                {
                    return true;
                }
                return false;
            }
            
            return false;
        }

        private bool TelephoneCheck(TelephoneHandlerModel model)
        {
            if(model != null)
            {
                if(model.Value >=0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        private bool RefIDCheck(RefIDHandlerModel model)
        {
            if(model != null)
            {
                if(model.Value != Guid.Empty)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool RegistrationModel(RegistrationViewModel viewModel)
        {
            if(viewModel != null)
            {
                EnumHandlerModel enumInstance = new(){Level = viewModel.Level, Department = viewModel.Department};
                RefIDHandlerModel idInstance = new(){Value = viewModel.RefID};
                TelephoneHandlerModel telephoneInstance = new(){Value = viewModel.Telephone};
                StringHandlerModel stringInstance = new(){FirstName = viewModel.FirstName, LastName = viewModel.LastName};

                if(StringCheck(stringInstance)&& EnumCheck(enumInstance) 
                && TelephoneCheck(telephoneInstance) && RefIDCheck(idInstance))
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
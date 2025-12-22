using System;
using System.Collections.Generic;
using System.Text;

namespace BackgroundProcessing.Application;

public interface IUserRegistrationService
{
    Task<RegistrationResult> RegisterAsync(RegisterationInput input);
}

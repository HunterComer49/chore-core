﻿using ChoreCore.Models;

namespace ChoreCore.Managers
{
    public interface IUserValidation
    {
        string ValidateUser(User user);
    }
}
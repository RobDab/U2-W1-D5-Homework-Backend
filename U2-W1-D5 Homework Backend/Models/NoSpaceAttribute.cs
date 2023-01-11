using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace U2_W1_D5_Homework_Backend.Models
{
    public class NoSpaceAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value.ToString().Contains(" "))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
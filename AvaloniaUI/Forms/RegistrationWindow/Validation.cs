using System;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.Net;
using System.Net.Mail;
class Validation
{
    public static bool CheckEverything(string name, string surname, string password, string repeatPassword, string email){
        if(CheckNameAndSurname(name, surname) && CheckPassword(password, repeatPassword) && CheckEmail(email)){
            return true;   
        }
        return false;
    }
    public static bool CheckNameAndSurname(string name, string surname){
        if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname)){
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Имя и фамилия должны быть заполнены", ButtonEnum.Ok).ShowWindowAsync();            
            return false;
        }
        return true;
    }
    public static bool CheckPassword(string password, string repeatPassword){
        if(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword)){
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поля для паролей должны быть заполнены", ButtonEnum.Ok).ShowWindowAsync();            
            return false;
        }
        if(password != repeatPassword){
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пароли не одинаковые", ButtonEnum.Ok).ShowWindowAsync();            
            return false;
        }
        if(password.Length < 8){
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Длина пароля должна быть длинее 8 символов", ButtonEnum.Ok).ShowWindowAsync();            
            return false;
        }
        return true;
    }
    public static bool CheckEmail(string email)
    {
        if(string.IsNullOrEmpty(email)){
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Адрес электронной почты должен быть заполнен", ButtonEnum.Ok).ShowWindowAsync();
            return false;
        }
        return true;
    }
}
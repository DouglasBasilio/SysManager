using System.ComponentModel;

namespace SysManager.Application.Errors
{
    public enum SysManagerErrors
    {
        // implementar lista de erros da regra de negócios
        [Description("Necessário informar a propriedade (Username)")]
        User_Post_BadRequest_UserName_Cannot_Be_Null_Or_Empty,

        [Description("Necessário informar a propriedade (Email)")]
        User_Post_BadRequest_Email_Cannot_Be_Null_Or_Empty,

        [Description("Necessário informar a propriedade (Password)")]
        User_Post_BadRequest_Password_Cannot_Be_Null_Or_Empty,

        [Description("Já existe um usuário cadastrado com esse e-mail")]
        User_Post_BadRequest_Email_Cannot_Be_Duplicated,
    }
}

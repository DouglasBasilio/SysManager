using System.ComponentModel;

namespace SysManager.Application.Errors
{
    public enum SysManagerErrors
    {
        // implementar lista de erros da regra de negócios
        #region User
        [Description("Necessário informar a propriedade (Username)")]
        User_Post_BadRequest_UserName_Cannot_Be_Null_Or_Empty,

        [Description("Necessário informar a propriedade (Email)")]
        User_Post_BadRequest_Email_Cannot_Be_Null_Or_Empty,

        [Description("Necessário informar a propriedade (Password)")]
        User_Post_BadRequest_Password_Cannot_Be_Null_Or_Empty,

        [Description("Já existe um usuário cadastrado com esse e-mail")]
        User_Post_BadRequest_Email_Cannot_Be_Duplicated,

        [Description("Usuário ou e-mail inválido ou inexistente")]
        User_Put_BadRequest_User_Not_Found,
        #endregion

        #region POST Unity
        [Description("É necessário informar o nome da unidade de medida")]
        Unity_Post_BadRequest_Name_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar se a unidade é ativa ou inativa")]
        Unity_Post_BadRequest_Active_Cannot_Be_Diferent_True_Or_False,

        [Description("Já existe uma unidade de medida com esse nome")]
        Unity_Post_BadRequest_Name_Cannot_Be_Duplicated,
        #endregion

        #region PUT Unity
        [Description("É necessário informar o id da unidade de medida")]
        Unity_Put_BadRequest_Id_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar o nome da unidade de medida")]
        Unity_Put_BadRequest_Name_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar se a unidade é ativa ou inativa")]
        Unity_Put_BadRequest_Active_Cannot_Be_Diferent_True_Or_False,

        [Description("Já existe uma unidade de medida com esse nome")]
        Unity_Put_BadRequest_Name_Cannot_Be_Duplicated,

        [Description("Unidade de medida inválida ou inexistente")]
        Unity_Put_BadRequest_Id_Is_Invalid_Or_Inexistent,
        #endregion

        #region DELETE Unity
        [Description("Unidade de medida inválida ou inexistente")]
        Unity_Delete_BadRequest_Id_Is_Invalid_Or_Inexistent,
        #endregion

        #region GET Unity
        [Description("Unidade de medida inexistente ou inválida")]
        Unity_Get_BadRequest_Id_Is_Invalid_Or_Inexistent,

        [Description("Nome da Unidade de medida não pode ser nulo ou vazio")]
        Unity_Get_BadRequest_Name_Cannot_Be_Null_Or_Empty,

        [Description("O campo 'Active' não pode estar vazio. Utilize 'todos', 'ativos' ou 'inativos')")]
        Unity_Get_BadRequest_Active_Cannot_Be_Empty,

        [Description("Página informada precisa ser maior que zero")]
        Unity_Get_BadRequest_Page_More_Than_Zero,

        [Description("Itens por página precisa ser maior do que zero")]
        Unity_Get_BadRequest_pageSize_More_Than_Zero,
        #endregion
    }
}

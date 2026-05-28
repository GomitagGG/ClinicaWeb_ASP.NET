namespace ClinicaWeb.Models;

/// <summary>
/// Modelo utilizado por la vista <c>Views/Shared/Error.cshtml</c> para mostrar
/// información de diagnóstico cuando ocurre un error HTTP no controlado.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// ID de la solicitud HTTP que causó el error.
    /// Puede ser nulo si no hay actividad de diagnóstico activa.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Indica si se debe mostrar el <see cref="RequestId"/> en la vista de error.
    /// Devuelve <c>true</c> solo si el ID no es nulo ni vacío.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

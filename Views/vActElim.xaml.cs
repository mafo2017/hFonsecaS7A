using hFonsecaS7A.Models;
using System.Diagnostics;
using System.Net;

namespace hFonsecaS7A.Views;

public partial class vActElim : ContentPage
{
	public vActElim(Estudiante datos)
	{
		InitializeComponent();

        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre.ToString();
        txtApellido.Text = datos.apellido.ToString();
        txtEdad.Text = datos.edad.ToString();
	}

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {

            var cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("codigo", txtCodigo.Text);
            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);

            // Agregamos logging para ver qu� se est� enviando
            string parametrosString = string.Join(", ", parametros.AllKeys.Select(k => $"{k}={parametros[k]}"));
            Debug.WriteLine($"Enviando par�metros: {parametrosString}");

            // Realizamos la petici�n PUT
            byte[] response = cliente.UploadValues(vEstudiante.Url, "PUT", parametros);

            // Convertimos la respuesta a string para verificar qu� nos devuelve el servidor
            string responseString = System.Text.Encoding.UTF8.GetString(response);
            Debug.WriteLine($"Respuesta del servidor: {responseString}");

           DisplayAlert("�xito", "Actualizaci�n realizada correctamente", "OK");
           Navigation.PushAsync(new vEstudiante());

        }
        catch (Exception ex)
        {

            DisplayAlert("Error", ex.Message, "Cerrar");
        }


    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("codigo", txtCodigo.Text);

            // Realizamos la petici�n DELETE
            byte[] response = cliente.UploadValues(vEstudiante.Url, "DELETE", parametros);

            // Convertimos la respuesta a string para verificar qu� nos devuelve el servidor
            string responseString = System.Text.Encoding.UTF8.GetString(response);
            Debug.WriteLine($"Respuesta del servidor: {responseString}");

            DisplayAlert("�xito", "Estudiante eliminado correctamente", "OK");
            Navigation.PushAsync(new vEstudiante());

        }
        catch (Exception ex)
        {

            DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }
}
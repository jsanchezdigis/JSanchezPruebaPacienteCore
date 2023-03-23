using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PacienteController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Paciente paciente = new ML.Paciente();
            ML.Result result = BL.Paciente.GetAll();

            if (result.Correct)
            {
                paciente.Pacientes = result.Objects;
                return View(paciente);
            }
            else
            {
                return View(paciente);
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdPaciente)
        {
            ML.Result resultTipoSangre = BL.TipoSangre.GetAll();

            ML.Paciente paciente = new ML.Paciente();
            paciente.TipoSangre = new ML.TipoSangre();


            if (resultTipoSangre.Correct)
            {
                paciente.TipoSangre.TipoSangres = resultTipoSangre.Objects;
            }
            if (IdPaciente == null)
            {
                return View(paciente);
            }
            else
            {
                //GetById
                ML.Result result = BL.Paciente.GetById(IdPaciente.Value);

                if (result.Correct)
                {
                    paciente = (ML.Paciente)result.Object;
                    paciente.TipoSangre.TipoSangres = resultTipoSangre.Objects;
                    return View(paciente);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            if (paciente.IdPaciente == 0)
            {
                //Add
                result = BL.Paciente.Add(paciente);

                if (result.Correct)
                {
                    ViewBag.Message = "Se completo el registro satisfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al insertar el registro";
                }
                return View("Modal");
            }
            else
            {
                //Update
                result = BL.Paciente.Update(paciente);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo el registro satisfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el registro";
                }
                return View("Modal");
            }
            return View("Modal");
        }

        [HttpGet]
        public ActionResult Delete(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            result = BL.Paciente.Delete(paciente);
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino el registro satisfactoriamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el registro";
            }
            return View("Modal");
        }
    }
}

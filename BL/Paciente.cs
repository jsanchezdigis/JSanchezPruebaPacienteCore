using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Paciente
    {
        public static ML.Result Add(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezPruebaPacienteContext context = new DL.JsanchezPruebaPacienteContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"PacienteAdd " +
                        $"'{paciente.Nombre}'," +
                        $"'{paciente.ApellidoPaterno}'," +
                        $"'{paciente.ApellidoMaterno}'," +
                        $"'{paciente.FechaNacimiento}'," +
                        $"'{paciente.TipoSangre.IdTipoSangre}'," +
                        $"'{paciente.Sexo}'," +
                        $"'{paciente.Diagnostico}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezPruebaPacienteContext context = new DL.JsanchezPruebaPacienteContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"PacienteUpdate " +
                        $"'{paciente.IdPaciente}'," +
                        $"'{paciente.Nombre}'," +
                        $"'{paciente.ApellidoPaterno}'," +
                        $"'{paciente.ApellidoMaterno}'," +
                        $"'{paciente.FechaNacimiento}'," +
                        $"'{paciente.TipoSangre.IdTipoSangre}'," +
                        $"'{paciente.Sexo}'," +
                        $"'{paciente.Diagnostico}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizo el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezPruebaPacienteContext context = new DL.JsanchezPruebaPacienteContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"PacienteDelete '{paciente.IdPaciente}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizo el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezPruebaPacienteContext context = new DL.JsanchezPruebaPacienteContext())
                {
                    var query = context.Pacientes.FromSqlRaw($"PacienteGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Paciente paciente = new ML.Paciente();

                            paciente.IdPaciente = obj.IdPaciente;
                            paciente.Nombre = obj.Nombre;
                            paciente.ApellidoPaterno = obj.ApellidoPaterno;
                            paciente.ApellidoMaterno = obj.ApellidoMaterno;
                            paciente.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");

                            paciente.TipoSangre = new ML.TipoSangre();
                            paciente.TipoSangre.IdTipoSangre = obj.IdTipoSangre.Value;
                            paciente.TipoSangre.Nombre = obj.TipoSangre;

                            paciente.Sexo = obj.Sexo;
                            paciente.FechaIngreso = obj.FechaIngreso.Value.ToString("dd-MM-yyyy");
                            paciente.Diagnostico = obj.Diagnostico;

                            result.Objects.Add(paciente);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdPaciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezPruebaPacienteContext context = new DL.JsanchezPruebaPacienteContext())
                {
                    var query = context.Pacientes.FromSqlRaw($"PacienteGetById '{IdPaciente}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.Paciente paciente = new ML.Paciente();

                            paciente.IdPaciente = obj.IdPaciente;
                            paciente.Nombre = obj.Nombre;
                            paciente.ApellidoPaterno = obj.ApellidoPaterno;
                            paciente.ApellidoMaterno = obj.ApellidoMaterno;
                            paciente.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");

                            paciente.TipoSangre = new ML.TipoSangre();
                            paciente.TipoSangre.IdTipoSangre = obj.IdTipoSangre.Value;
                            paciente.TipoSangre.Nombre = obj.TipoSangre;

                            paciente.Sexo = obj.Sexo;
                            paciente.FechaIngreso = obj.FechaIngreso.Value.ToString("dd-MM-yyyy"); ;
                            paciente.Diagnostico = obj.Diagnostico;

                            result.Object = paciente;
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

    }
}
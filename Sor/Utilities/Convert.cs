using Sor.Models;
using Sor.Models.EDM;

namespace Sor.Utilities
{
    public class ConvertJava
    {
        public static DocenteScoreJava NetToJava(DetalleTerna docenteScore)
        {
            DocenteScoreJava objJava = new DocenteScoreJava()
            {
                docenteScoreId = docenteScore.DocenteScoreId,
                nivelEstudio = docenteScore.NivelEstudio,
                porcentaje = docenteScore.Porcentaje,
                nombreDocente = docenteScore.NombreDocente,
                horasActuales = docenteScore.HorasActuales,
                evaluacion = docenteScore.EvaluacionEstudiante,
                cargaNotas = docenteScore.CargaNotas,
                ausentismos = docenteScore.Ausentismos
            };

            return objJava;
            
        }
    }
}
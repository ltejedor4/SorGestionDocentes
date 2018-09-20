using Sor.Models;
using Sor.Models.EDM;

namespace Sor.Utilities
{
    public class ConvertJava
    {
        public static DocenteScoreJava NetToJava(DocenteScore docenteScore)
        {
            DocenteScoreJava objJava = new DocenteScoreJava()
            {
                docenteScoreId = docenteScore.DocenteScoreId,
                nivelEstudio = docenteScore.NivelEstudio,
                porcentaje = docenteScore.Porcentaje,
                nombreDocente = docenteScore.NombreDocente,
                horasActuales = docenteScore.HorasActuales,
                evaluacion = docenteScore.Evaluacion,
                cargaNotas = docenteScore.CargaNotas,
                ausentismos = docenteScore.Ausentismos
            };

            return objJava;
            
        }
    }
}
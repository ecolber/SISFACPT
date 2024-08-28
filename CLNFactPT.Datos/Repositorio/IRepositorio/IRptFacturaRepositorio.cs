using CLNFactPT.Datos.DTOs;
using CLNFactPT.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.Repositorio.IRepositorio
{
    public interface IRptFacturaRepositorio 
    {
        IEnumerable<RptFacturaDTO> ObtenerReporteFacturas();
    }
}

using System.Drawing;
using Sim.Application.WebService.RFB.Models;

namespace Sim.Application.WebService.RFB.Interfaces;

public interface IServiceRFB {
    string Captcha();
    Task<ECompany> Search(string document, string captcha);
}
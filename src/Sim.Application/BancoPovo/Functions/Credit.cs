using Sim.Application.BancoPovo.ViewModel;

namespace Sim.Application.BancoPovo.Functions;

public static class Credit {

    // Returna somatória dos valores de créditos
    public static decimal Totalize(this IEnumerable<VMContrato> list) {
        var _soma = 0.0M;
        foreach(var c in list) {
            _soma += c.Valor;
        }
        return _soma;
    }
}
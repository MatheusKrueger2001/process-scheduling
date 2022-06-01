using EscalonamentoDeProcessos;

var processoLista = new List<Processos>();

Console.WriteLine("Bem vindo ao Programa de Escalonamento de processos! :)");
Console.WriteLine("--------------------------//--------------------------");

Console.WriteLine("Qual técnica você deseja utilizar?");
Console.WriteLine("1 - Técnica FIFO(First In First Out)");
Console.WriteLine("2 - Técnica Menor Processo");
Console.WriteLine("3 - Técnica Prioridades");
Console.WriteLine("4 - Técnica Round Robin(Ciclos)");

int tecnica = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Parar de inserir processos e obter o resultado, digite !");
Console.WriteLine("Digite aqui seus Processos: ");

do
{
    var processo = new Processos();
    Console.Write("Processo: ");
    processo.Nome = Console.ReadLine();
    if (!Continuar(processo.Nome))
        break;

    Console.Write("Tempo total do processo (ms): ");
    var resposta = Console.ReadLine();
    if (!Continuar(resposta))
        break;
    processo.Tempo = Convert.ToInt32(resposta);

    if (tecnica == 3)
    {
        //Console.Write("Prioridade: ");
        //processo.Prioridade = Convert.ToInt32(Console.ReadLine());
        //var resposta = Console.ReadLine();
        //if (!Continuar(resposta))
        //    break;
        //processo.Tempo = Convert.ToInt32(resposta);
    }

    processoLista.Add(processo);


} while (true);

switch (tecnica)
{
    case 1:
        Fifo();
        break;
    case 2:
        MenorProcesso();
        break;
    case 3:
        Prioridades();
        break;
    case 4:
        RoundRobin();
        break;
    default:
        Console.WriteLine("Valor incorreto, tente novamente :)");
        break;
}


void Fifo()
{
    var tempos = Tempos();

    DiagramaGannt(tempos);
    TempoDeEsperaMedio(tempos);
    TempoMedioDeProcessamento(tempos);
}

void MenorProcesso()
{
    BubbleSortAscendingTempo();
    var tempos = Tempos();

    DiagramaGannt(tempos);
    TempoDeEsperaMedio(tempos);
    TempoMedioDeProcessamento(tempos);
}

void Prioridades()
{
    BubbleSortDescendingPrioridade();
    var tempos = Tempos();

    DiagramaGannt(tempos);
    TempoDeEsperaMedio(tempos);
    TempoMedioDeProcessamento(tempos);
}

void RoundRobin()
{
    Console.WriteLine("Escreva o Quantum (ms): ");
    var quantum = Convert.ToInt32(Console.ReadLine());

    var tempos = Temposx();

    var x = new List<Processos>();

        var controleTempo = 1;
    while (processoLista.Count != 0)
    {
        for (int i = 0; i < processoLista.Count; i++)
        {
            Processos result = null;
            if (x.Count > 0)
            {
                result = x.Last();
            }

            var pTemp = new Processos();
            pTemp.Nome = processoLista[i].Nome;

            if (tempos[i + controleTempo] >= quantum)
            {
                tempos[i + controleTempo] -= quantum;

                if (tempos[i + controleTempo] == 0)
                {
                    processoLista.RemoveAt(i);
                    controleTempo++;
                    i--;
                }

                if (result != null)
                    pTemp.Tempo = result.Tempo + quantum;

                else
                    pTemp.Tempo += quantum;
            }

            else
            {

                if (result != null)
                    pTemp.Tempo = result.Tempo + tempos[i + controleTempo];

                else
                    pTemp.Tempo += tempos[i + controleTempo];


                tempos[i + controleTempo] = 0;
                processoLista.RemoveAt(i);
                controleTempo++;
                i--;
            }


            x.Add(pTemp);



        }
    }

    foreach (var item in x)
    {
        Console.WriteLine(item);
    }

}

///////////////////////////////////////////////////////////

void DiagramaGannt(int[] tempos)
{
    int quebraDeLinha = 0;

    int i = 0;
    Console.Write($"{tempos[i]}      ");
    foreach (var item in processoLista)
    {
        i++;
        Console.Write($"[{item.Nome}]      {tempos[i]}      ");

        quebraDeLinha++;
        if (quebraDeLinha == 5)
        {
            Console.WriteLine("\n\n");
            Console.Write($"{tempos[i]}      ");
            quebraDeLinha = 0;
        }
    }

    Console.WriteLine();

    //foreach (var item in tempos)
    //{
    //    Console.Write($"{item}");

    //}


    //for (int i = 0; i < tempos.Length; i++)
    //{
    //    Console.WriteLine($"         {processoLista[i].Nome}  ");
    //    Console.Write($"  {tempos[i]}  ");
    //}



    //Console.Write("0 ");
    //foreach (var item in tempos)
    //{
    //    Console.Write($"{item}");
    //    quebraDeLinha++;
    //    if (quebraDeLinha == 5)
    //    {
    //        Console.WriteLine();
    //        quebraDeLinha = 0;
    //    }
    //}
    Console.WriteLine("\n");
}

void BubbleSortAscendingTempo()
{
    for (int i = (processoLista.Count - 1); i >= 0; i--)
    {
        for (int j = 1; j <= i; j++)
        {
            if (processoLista[j - 1].Tempo > processoLista[j].Tempo)
            {
                var temp = processoLista[j - 1];
                processoLista[j - 1] = processoLista[j];
                processoLista[j] = temp;
            }
        }
    }
}

void BubbleSortDescendingPrioridade()
{
    for (int i = (processoLista.Count - 1); i >= 0; i--)
    {
        for (int j = 1; j <= i; j++)
        {
            if (processoLista[j - 1].Prioridade < processoLista[j].Prioridade)
            {
                var temp = processoLista[j - 1];
                processoLista[j - 1] = processoLista[j];
                processoLista[j] = temp;
            }
        }
    }
}

int[] Tempos()
{
    int[] tempos = new int[processoLista.Count + 1];

    for (int i = 1; i < tempos.Length; i++)
    {
        var tempoAnterior = tempos[i - 1];

        tempos[i] = tempoAnterior + processoLista[i - 1].Tempo;
    }
    return tempos;
}

int[] Temposx()
{
    int[] tempos = new int[processoLista.Count + 1];

    for (int i = 1; i < tempos.Length; i++)
    {
        tempos[i] = processoLista[i - 1].Tempo;
    }
    return tempos;
}


bool Continuar(string resposta)
{
    if (resposta == "!")
    {
        return false;
    }
    return true;
}

double TempoDeEsperaMedio(int[] tempos)
{
    double TEM = 0;
    for (int i = 0; i < tempos.Length - 1; i++)
    {
        TEM += tempos[i];
    }
    TEM = TEM / (tempos.Length - 1);
    Console.WriteLine($"Tempo de espera médio: {TEM}(ms)");
    return TEM;
}

double TempoMedioDeProcessamento(int[] tempos)
{
    double TMP = 0;
    for (int i = 1; i < tempos.Length; i++)
    {
        TMP += tempos[i];
    }
    TMP = TMP / (tempos.Length - 1);
    Console.WriteLine($"Tempo médio de processamento: {TMP}(ms)");
    return TMP;
}
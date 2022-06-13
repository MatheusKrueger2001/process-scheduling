﻿using EscalonamentoDeProcessos;
using System;
using System.Collections.Generic;
using System.Linq;

Main();

void Main()
{

    var processoLista = new List<Processos>();
    Console.WriteLine("Bem vindo ao Programa de Escalonamento de processos! :)");
    Console.WriteLine("--------------------------//--------------------------");

    Console.WriteLine("Qual técnica você deseja utilizar?");
    Console.WriteLine("1 - Técnica FIFO(First In First Out)");
    Console.WriteLine("2 - Técnica Menor Processo");
    Console.WriteLine("3 - Técnica Prioridades");
    Console.WriteLine("4 - Técnica Round Robin(Ciclos)");
    Console.WriteLine("5 - Técnica  Múltiplas Filas");

    int tecnica = Convert.ToInt32(Console.ReadLine()); //Captura a tecnica desejada pelo usuário convertendo em inteiro

    Console.WriteLine("Parar de inserir processos e obter o resultado,  apenas deixe o campo em branco");
    Console.WriteLine("Digite aqui seus Processos: ");

    do
    {
        var processo = new Processos(); // Instancia um novo objeto do tipo Processos
        Console.Write("Processo: ");
        processo.Nome = Console.ReadLine(); // Leitura propriedade Nome
        if (String.IsNullOrEmpty(processo.Nome))
            break;

        Console.Write("Tempo total do processo (ms): ");

        var tempo = Console.ReadLine();

        if (String.IsNullOrEmpty(tempo))
            break;

        processo.Tempo = Convert.ToInt32(tempo);


        if (tecnica == 3 || tecnica == 5)
        {
            Console.Write("Prioridade: ");
            var prioridade = Console.ReadLine();
            if (String.IsNullOrEmpty(prioridade))
                break;

            processo.Prioridade = Convert.ToInt32(prioridade);
        }

        if (tecnica == 5)
        {
            Console.Write("Tipo da Fila: ");
            Console.WriteLine("***Tipos de filas existentes, escolha uma por favor***");
            Console.WriteLine("1 - Técnica FIFO(First In First Out)");
            Console.WriteLine("2 - Técnica Menor Processo");
            Console.WriteLine("3 - Técnica Prioridades");
            Console.WriteLine("4 - Técnica Round Robin(Ciclos)");

            processo.TipoFila = Convert.ToInt32(Console.ReadLine());

        }

        processoLista.Add(processo);
        Console.Write("\n");

    } while (true);

    //Switch case, executa determinada função de acordo com a tecnica desejada
    switch (tecnica)
    {
        case 1:
            Fifo(processoLista);
            break;
        case 2:
            MenorProcesso(processoLista);
            break;
        case 3:
            Prioridades(processoLista);
            break;
        case 4:
            RoundRobin(processoLista);
            break;
        case 5:
            multiplasLinhas(processoLista);
            break;
        default:
            Console.WriteLine("Valor incorreto, tente novamente :)");
            break;
    }

    Console.WriteLine("\n---------------------//----------------------\n");
    Main();
}

void Fifo(List<Processos> processoLista)
{
    var tempos = Tempos(processoLista);

    DiagramaGannt(tempos, processoLista);
    TempoDeEsperaMedio(tempos);
    TempoMedioDeProcessamento(tempos);
}

void MenorProcesso(List<Processos> processoLista)
{
    BubbleSortAscendingTempo(processoLista);
    var tempos = Tempos(processoLista);

    DiagramaGannt(tempos, processoLista);
    TempoDeEsperaMedio(tempos);
    TempoMedioDeProcessamento(tempos);
}

void Prioridades(List<Processos> processoLista)
{
    BubbleSortDescendingPrioridade(processoLista);
    var tempos = Tempos(processoLista);

    DiagramaGannt(tempos, processoLista);
    TempoDeEsperaMedio(tempos);
    TempoMedioDeProcessamento(tempos);
}

void RoundRobin(List<Processos> processoLista)
{
    Console.Write("Escreva o Quantum (ms): ");
    string quantumValue = Console.ReadLine();
    if (String.IsNullOrEmpty(quantumValue))
        Main();

    var quantum = Convert.ToInt32(quantumValue);

    var tempos = Temposx(processoLista);

    var aux = new List<Processos>();

    var processoListaAux = new List<Processos>();
    for (int i = 0; i < processoLista.Count; i++)
    {
        processoListaAux.Add(processoLista[i]);
    }

    //var processoListaAu = processoLista;

    int controleTempo = 1;

    while (processoListaAux.Count != 0)
    {
        for (int i = 0; i < processoListaAux.Count; i++)
        {
            Processos result = null;
            if (aux.Count > 0)
            {
                result = aux.Last();
            }

            var pTemp = new Processos();
            pTemp.Nome = processoListaAux[i].Nome;

            if (tempos[i + controleTempo] >= quantum)
            {
                tempos[i + controleTempo] -= quantum;

                if (tempos[i + controleTempo] == 0)
                {
                    processoListaAux.RemoveAt(i);
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
                processoListaAux.RemoveAt(i);
                controleTempo++;
                i--;
            }
            aux.Add(pTemp);
        }
    }

    var temposProcessos = new int[aux.Count];
    for (int i = 0; i < aux.Count; i++)
    {
        temposProcessos[i] = aux[i].Tempo;
    }

    DiagramaGannt(temposProcessos, processoLista);
    TempoDeEsperaMedio(temposProcessos);
    CalculoTempoMedioRoundRobin(tempos, quantum);
}


void multiplasLinhas(List<Processos> processoLista) 
{
    
    var processoListaFifo = new List<Processos>();
    var processoListaMenorProcesso = new List<Processos>();
    var processoListaPrioridades = new List<Processos>();
    var processoListaRoundRobin = new List<Processos>();
    var prioridadeFila = new List();

    for (int a = 0; a < processoLista.Count; a++) {
               
        //Switch case, executa determinada função de acordo com a tecnica desejada
        switch (processo[a].TipoFila)
        {
            case 1:
                var processoListaFifo[a] = processo[a];
                var prioridadeFila = processo[a].Prioridade;
                break;
            case 2:
                var processoListaMenorProcesso[a] = processo[a];
                var prioridadeFila = processo[a].Prioridade;
                break;
            case 3:
                var processoListaPrioridades[a] = processo[a];
                var prioridadeFila = processo[a].Prioridade;
                break;
            case 4:
                var processoListaRoundRobin[a] = processo[a];
                var prioridadeFila = processo[a].Prioridade;
                break;
            default:
                var processoListaFifo[a] = processo[a];
                var prioridadeFila = processo[a].Prioridade;
                break;
        }
    }
}

///////////////////////////////////////////////////////////

void DiagramaGannt(int[] tempos, List<Processos> processoLista)
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

void BubbleSortAscendingTempo(List<Processos> processoLista)
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

void BubbleSortDescendingPrioridade(List<Processos> processoLista)
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

int[] Tempos(List<Processos> processoLista)
{
    int[] tempos = new int[processoLista.Count + 1];

    for (int i = 1; i < tempos.Length; i++)
    {
        var tempoAnterior = tempos[i - 1];

        tempos[i] = tempoAnterior + processoLista[i - 1].Tempo;
    }
    return tempos;
}

int[] Temposx(List<Processos> processoLista)
{
    int[] tempos = new int[processoLista.Count + 1];

    for (int i = 1; i < tempos.Length; i++)
    {
        tempos[i] = processoLista[i - 1].Tempo;
    }
    return tempos;
}

void CalculoTempoMedioRoundRobin(int[] tempos, int quantum)
{
    int[] temposFinal = new int[quantum];
    int[] mod = new int[quantum];
    int qtdAux = 0;

    for (int i = 0; i < tempos.Length; i++)
    {
        mod[i] = (tempos[i] % quantum);
        temposFinal[i] = (tempos[i] - mod[i]);
        qtdAux = qtdAux + (temposFinal[i] / quantum);
        if (mod[i] > 0)
        {
            qtdAux++;
        }

    }

    int somaMedia = 0;
    do
    {
        for (int a = 0; a < tempos.Length; a++)
        {
            temposFinal[a] -= quantum;

            if (temposFinal[a] == 0 && mod[a] > 0)
            {
                somaMedia += mod[a];
            }
            else if (temposFinal[a] > 0)
            {
                somaMedia += quantum;
            }
        }

        qtdAux--;
    } while (qtdAux == 0);

    var TMP = somaMedia / tempos.Length;
    Console.WriteLine($"Tempo médio de processamento: {TMP}(ms)");
}
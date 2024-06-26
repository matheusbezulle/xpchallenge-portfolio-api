﻿using System.Diagnostics;

namespace XpChallenge.Portfolio.Application.DataTransfer
{
    public class ResponseBaseDto(bool erro = false)
    {
        public bool Erro { get; set; } = erro;
        public List<string> Mensagens { get; set; } = [];
        public string CorrelationId { get; set; } = Activity.Current?.Id ?? string.Empty;
    }
}

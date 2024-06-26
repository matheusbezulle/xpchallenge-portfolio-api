﻿using Microsoft.AspNetCore.Mvc;
using XpChallenge.Portfolio.Application.DataTransfer;
using XpChallenge.Portfolio.Application.Notifications;

namespace XpChallenge.Portfolio.Api.Controllers
{
    public abstract class ControllerBase(INotificator notificator) : Controller
    {
        private readonly INotificator _notificator = notificator;

        protected IActionResult ProcessarRetorno(object response = null)
        {
            var responseBase = new ResponseBaseDto
            {
                Mensagens = _notificator.ObterMensagens()
            };

            if (_notificator.PossuiErros())
            {
                responseBase.Erro = true;

                if (_notificator.PossuiErrosNegocio())
                {
                    return StatusCode(422, responseBase);
                }

                if (_notificator.PossuiErrosAplicacao())
                {
                    return StatusCode(500, responseBase);
                }
            }

            if (response != null)
                return Ok(response);

            return Ok(responseBase);
        }
    }
}

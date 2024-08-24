using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayAudioOnSelect : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        if (AudioManager.instance.sfxSource != null && AudioManager.instance.sfxSource.isPlaying)
        {
            AudioManager.instance.sfxSource.Stop();
        }

        // Verifica qual botao foi selecionado e reproduz o audio correspondente
        switch (gameObject.name)
        {
            case "jogarButton":
                AudioManager.instance.PlaySFX("Jogar");
                break;

            case "opcoesButton":
                AudioManager.instance.PlaySFX("Opcoes");
                break;

            case "sairButton":
                AudioManager.instance.PlaySFX("Sair");
                break;

            case "BarraVolume":
                AudioManager.instance.PlaySFX("Volume");
                break;

            case "BarraSensibilidade":
                AudioManager.instance.PlaySFX("Jogar");
                break;

            case "BarraSFX":
                AudioManager.instance.PlaySFX("SFX");
                break;

            case "toggleLegenda":
                AudioManager.instance.PlaySFX("Legendas");
                break;

            case "toggleAudiodescricao":
                AudioManager.instance.PlaySFX("Audiodescricao");
                break;

            case "voltarButton":
                AudioManager.instance.PlaySFX("Voltar");
                break;

            default:
                Debug.LogWarning("Nenhum �udio atribu�do para este objeto.");
                break;
        }
    }
}

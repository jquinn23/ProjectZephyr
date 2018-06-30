using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FontDisplay : MonoBehaviour
{
    public Text blockyTxt;
    public Text blockyTxtHeader;
    public Text blockyLiteTxt;
    public Text blockyLiteTxtHeader;
    public GameObject blockyLite;
    public GameObject blockyLiteHeader;
    public GameObject blocky;
    public GameObject blockyHeader;

    public GameObject fontCanvas;
    public GameObject mainCamera;
    public GameObject worldCamera;

	
	void Start ()
    {
        blockyTxtHeader.text = "JazzCreate Blocky Font";
        blockyTxt.text = " 193 Usable Characters " + "\n !#$%&'()*+,-./0123456789:;<=>?@ " + " ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^ _`abcdefghijklmnopqrstuvwxyz " +
    "¢£¤¥©ª«®º»ÀÁÂÃÄÅÆÇÈÉÊË \n ÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞß " + " àáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ\nŒœŠšŽžƒ•<>€™←↑→↓▲►▼◄☺☻☼♀♂♠♣♥♦♪♫ ";
        blockyLiteTxt.text = "\n JazzCreateBlocky(lite) \n abcdefghijklm \n nopqrstuvwxyz \n 1234567890 \n ABCDEFGHIJKLM \n NOPQRSTUVWXYZ \n !#$%&'()*+,-./:;<=>?@ \n ^_`~¢£§©«»’—‘’•€";
        blockyLiteTxtHeader.text = "JazzCreate Blocky(lite) Font";
        blocky.SetActive(true);
        blockyHeader.SetActive(true);
        blockyLite.SetActive(false);
        blockyLiteHeader.SetActive(false);
        fontCanvas.SetActive(true);
        mainCamera.SetActive(true);
        worldCamera.SetActive(false);

        StopCoroutine("CycleFonts");
        StartCoroutine("CycleFonts");

    }
    IEnumerator CycleFonts()
    {
        blocky.SetActive(true);
        blockyHeader.SetActive(true);
        blockyLite.SetActive(false);
        blockyLiteHeader.SetActive(false);
        mainCamera.SetActive(true);
        worldCamera.SetActive(false);
        yield return new WaitForSeconds(5);
        blocky.SetActive(false);
        blockyHeader.SetActive(false);
        blockyLite.SetActive(true);
        blockyLiteHeader.SetActive(true);
        //mainCamera.SetActive(true);
       // worldCamera.SetActive(false);
        yield return new WaitForSeconds(5);
        fontCanvas.SetActive(false);
        blocky.SetActive(false);
        blockyHeader.SetActive(false);
        blockyLite.SetActive(false);
        blockyLiteHeader.SetActive(false);
        mainCamera.SetActive(false);
        worldCamera.SetActive(true);
    }
}

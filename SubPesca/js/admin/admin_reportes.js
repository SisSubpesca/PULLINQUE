
function mostrarPestanas(numPestana) {

  

    if (numPestana == 1) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }


        try {
            document.getElementById("seccion1").style.display = '';
        }catch(e){}

        try {
            document.getElementById("seccion2").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion3").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion4").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion5").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion6").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion7").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion8").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion9").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion10").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion11").style.display = 'none';
        }catch(e){}

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        }catch(e){}

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        }catch(e){}

    }



    if (numPestana == 2) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }


        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }

    }

    if (numPestana == 3) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }

    if (numPestana == 4) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }
        
        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }

    if (numPestana == 5) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }

    if (numPestana == 6) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }

    if (numPestana == 7) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }
        
        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }
        
        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }
        
        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }

    if (numPestana == 8) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className  = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }
        
        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }
        
        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }

    if (numPestana == 9) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }

    if (numPestana == 10) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }   

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }
        
        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }


    if (numPestana == 11) {

        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }


        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11_selected';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = '';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }





    if (numPestana == 14) { //Suficiencia Formal


        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = ''; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = 'none'; //Evaluación URB
        } catch (e) { }
    }


    if (numPestana == 15) { //Evaluación URB


        if (document.getElementById("ctl00_rightbody_lnk_EvaluacionURB") != null) {
            document.getElementById("ctl00_rightbody_lnk_EvaluacionURB").className = 'tab1_selected';
        }

        if (document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal") != null) {
            document.getElementById("ctl00_rightbody_lnk_SuficienciaFormal").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InfCartografia") != null) {
            document.getElementById("ctl00_rightbody_lnk_InfCartografia").className = 'tab1';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InspTerreno") != null) {
            document.getElementById("ctl00_rightbody_lnk_InspTerreno").className = 'tab2';
        }

        if (document.getElementById("ctl00_rightbody_lnk_BancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_BancoNatural").className = 'tab3';
        }

        if (document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural") != null) {
            document.getElementById("ctl00_rightbody_lnk_DifusionBancoNatural").className = 'tab4';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Difrol") != null) {
            document.getElementById("ctl00_rightbody_lnk_Difrol").className = 'tab5';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeSEA") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeSEA").className = 'tab6';
        }

        if (document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios") != null) {
            document.getElementById("ctl00_rightbody_lnk_AntecedentesComplementarios").className = 'tab7';
        }

        if (document.getElementById("ctl00_rightbody_lnk_Planos") != null) {
            document.getElementById("ctl00_rightbody_lnk_Planos").className = 'tab8';
        }

        if (document.getElementById("ctl00_rightbody_lnk_InformeDAC") != null) {
            document.getElementById("ctl00_rightbody_lnk_InformeDAC").className = 'tab9';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSP") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSP").className = 'tab10';
        }

        if (document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA") != null) {
            document.getElementById("ctl00_rightbody_lnk_ResolucionSSFFAA").className = 'tab11';
        }

        try {
            document.getElementById("seccion1").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion2").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion3").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion4").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion5").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion6").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion7").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion8").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion9").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion10").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion11").style.display = 'none';
        } catch (e) { }

        try {
            document.getElementById("seccion14").style.display = 'none'; //Suficiencia Formal
        } catch (e) { }

        try {
            document.getElementById("seccion15").style.display = ''; //Evaluación URB
        } catch (e) { }

    }
}

function onlyNumeric(obj) {
    var valor = obj.value;
    obj.value = valor.replace(/[^0-9]/g, "");
}



function calendario (textbox_fecha, icono_calendario)
{

    try {

        var LEFT_CAL = Calendar.setup({
            weekNumbers: false,
            selectionType: Calendar.SEL_MULTIPLE,
            showTime: 12,
            animation: false

        });
        new Calendar({
            inputField: textbox_fecha,
            dateFormat: "%d/%m/%Y",
            trigger: icono_calendario,
            bottomBar: false,
            onSelect: function () {
                var date = Calendar.intToDate(this.selection.get());
                LEFT_CAL.args.max = date;
                LEFT_CAL.redraw();
                this.hide();
            }
        });
    } catch (e) {
        //alert(e);
    }
    
}

function calendarioConHora(textbox_fecha, icono_calendario) {
    var LEFT_CAL = Calendar.setup({
        weekNumbers: false,
        selectionType: Calendar.SEL_MULTIPLE,
        showTime: 12,
        animation: false

    });
    new Calendar({
        inputField: textbox_fecha,
        dateFormat: "%d/%m/%Y %H:%M",
        trigger: icono_calendario,
        bottomBar: false,
        onSelect: function () {
            var date = Calendar.intToDate(this.selection.get());
            LEFT_CAL.args.max = date;
            LEFT_CAL.redraw();
            this.hide();



            if (textbox_fecha != null && textbox_fecha == 'ctl00_rightbody_PlazoInicio') {
                if (document.getElementById("ctl00_rightbody_NumeroPlazo") != null) {
                   
                    //chrome/firefox
                   try{
                        var element = document.getElementById('ctl00_rightbody_NumeroPlazo');
                        var event = new Event('change');
                        element.dispatchEvent(event);
                    }catch(e){
                        //IE
                        document.getElementById("ctl00_rightbody_NumeroPlazo").onchange(); 
                    }


                }
            }


            if (textbox_fecha != null && textbox_fecha == 'PlazoInicio') {
                if (document.getElementById("NumeroPlazo") != null) {

                    //chrome/firefox
                    try {
                        var element = document.getElementById('NumeroPlazo');
                        var event = new Event('change');
                        element.dispatchEvent(event);
                    } catch (e) {
                        //IE
                        document.getElementById("NumeroPlazo").onchange();
                    }


                }
            }

        }
    });
}

function calendarioConHoraChange(textbox_fecha, icono_calendario, idFechaCI) {
    var LEFT_CAL = Calendar.setup({
        
        weekNumbers: false,
        selectionType: Calendar.SEL_MULTIPLE,
        showTime: 12,
        animation: false

    });
    new Calendar({
        inputField: textbox_fecha,
        dateFormat: "%d/%m/%Y %H:%M",
        trigger: icono_calendario,
        bottomBar: false,
        onSelect: function () {
            var date = Calendar.intToDate(this.selection.get());
            LEFT_CAL.args.max = date;
            LEFT_CAL.redraw();
            this.hide();
            //__doPostBack(idFechaCI, "ontextchanged");
        }
    });
}



function calendarioCI(textbox_fecha, icono_calendario,idNumeroCI) {
    var LEFT_CAL = Calendar.setup({
        weekNumbers: false,
        selectionType: Calendar.SEL_MULTIPLE,
        showTime: 12,
        animation: false

    });
    new Calendar({
        inputField: textbox_fecha,
        dateFormat: "%d/%m/%Y",
        trigger: icono_calendario,
        bottomBar: false,
        onSelect: function () {
            var date = Calendar.intToDate(this.selection.get());
            LEFT_CAL.args.max = date;
            LEFT_CAL.redraw();
            this.hide();
            __doPostBack(idNumeroCI, "onchange");
            //__doPostBack("ctl00_rightbody_antecedDelSectorComponente_capitaniaPuerto_NumeroCI", "onchange");
            
        }
    });
}


function invoca_calendarios(pagina)
{
    switch(pagina)
    {
        case "busquedaSolicitudes":
            calendario("ctl00_rightbody_FechaDesde", "imgFechaDesde");
            calendario("ctl00_rightbody_FechaHasta", "imgFechaHasta");
            break;
        case "resumenIndicadores":

            if (document.getElementById("ctl00_rightbody_Ind01_FechaDesde") != null) {
                calendario("ctl00_rightbody_Ind01_FechaDesde", "Ind01_imgFechaDesde");
            }
            if (document.getElementById("ctl00_rightbody_Ind01_FechaHasta") != null) {
                calendario("ctl00_rightbody_Ind01_FechaHasta", "Ind01_imgFechaHasta");
            }
            if (document.getElementById("ctl00_rightbody_Ind02_FechaDesde") != null) {
                calendario("ctl00_rightbody_Ind02_FechaDesde", "Ind02_imgFechaDesde");
            }
            if (document.getElementById("ctl00_rightbody_Ind02_FechaHasta") != null) {
                calendario("ctl00_rightbody_Ind02_FechaHasta", "Ind02_imgFechaHasta");
            }
            if (document.getElementById("ctl00_rightbody_Ind03_FechaConsulta") != null) {
                calendario("ctl00_rightbody_Ind03_FechaConsulta", "Ind03_imgFechaConsulta");
            }
            if (document.getElementById("ctl00_rightbody_Ind03_FechaDesde") != null) {
                calendario("ctl00_rightbody_Ind03_FechaDesde", "Ind03_imgFechaDesde");
            }
            if (document.getElementById("ctl00_rightbody_Ind03_FechaHasta") != null) {
                calendario("ctl00_rightbody_Ind03_FechaHasta", "Ind03_imgFechaHasta");
            }
            if (document.getElementById("ctl00_rightbody_Ind04_FechaDesde") != null) {
                calendario("ctl00_rightbody_Ind04_FechaDesde", "Ind04_imgFechaDesde");
            }
            if (document.getElementById("ctl00_rightbody_Ind04_FechaHasta") != null) {
                calendario("ctl00_rightbody_Ind04_FechaHasta", "Ind04_imgFechaHasta");
            }
            if (document.getElementById("ctl00_rightbody_Ind05_FechaDesde") != null) {
                calendario("ctl00_rightbody_Ind05_FechaDesde", "Ind05_imgFechaDesde");
            }
            if (document.getElementById("ctl00_rightbody_Ind05_FechaHasta") != null) {
                calendario("ctl00_rightbody_Ind05_FechaHasta", "Ind05_imgFechaHasta");
            }
            if (document.getElementById("ctl00_rightbody_Ind06_FechaDesde") != null) {
                calendario("ctl00_rightbody_Ind06_FechaDesde", "Ind06_imgFechaDesde");
            }
            if (document.getElementById("ctl00_rightbody_Ind06_FechaHasta") != null) {
                calendario("ctl00_rightbody_Ind06_FechaHasta", "Ind06_imgFechaHasta");
            }
            if (document.getElementById("ctl00_rightbody_Ind07_FechaDesde") != null) {
                calendario("ctl00_rightbody_Ind07_FechaDesde", "Ind07_imgFechaDesde");
            }
            if (document.getElementById("ctl00_rightbody_Ind07_FechaHasta") != null) {
                calendario("ctl00_rightbody_Ind07_FechaHasta", "Ind07_imgFechaHasta");
            }
            break;
        case "formMigracion":
            calendario("ctl00_rightbody_F_FECSOLI", "img1");
            calendario("ctl00_rightbody_F_FECCISSP", "img2");
            calendario("ctl00_rightbody_F_FECOFISE", "img3");
            calendario("ctl00_rightbody_F_FECHAINFCART", "img4");
            calendario("ctl00_rightbody_F_FECHAOFICIOINSPTERR", "img5");
            calendario("ctl00_rightbody_F_FECHACIINSPTERRENO", "img6");
            calendario("ctl00_rightbody_F_FECHAINGRESOINSTERRDAC", "img7");
            calendario("ctl00_rightbody_F_FECHAINFBN", "img8");
            calendario("ctl00_rightbody_F_FECHAMEMOUD", "img9");
            calendario("ctl00_rightbody_F_FECHAPUBLICACIONRADIAL", "img10");
            calendario("ctl00_rightbody_F_FECHACICERTRADIAL", "img11");
            calendario("ctl00_rightbody_F_FECHADOCSEA", "img12");
            calendario("ctl00_rightbody_F_FECHACICARTARESP", "img13");
            calendario("ctl00_rightbody_F_FECHACARTAAMBIENTAL", "img14");
            calendario("ctl00_rightbody_F_FECHARCAFECHAMO", "img15");
            calendario("ctl00_rightbody_F_FECHAITDAC", "img16");
            calendario("ctl00_rightbody_F_FECHADEVOLUCIONDJ", "img17");
            calendario("ctl00_rightbody_F_FECHAITCOMPLEMENTARIO", "img18");
            calendario("ctl00_rightbody_F_FECHARESOLSSP", "img19");
            calendario("ctl00_rightbody_F_FECHADSMO", "img20");
            calendario("ctl00_rightbody_F_FECHAOFICIODEVUELTASSFFAA", "img21");
            calendario("ctl00_rightbody_F_FECHANUMCIDEVOL", "img22");
            calendario("ctl00_rightbody_F_FECHAOFICIORESOL", "img23");
            calendario("ctl00_rightbody_S_FECHA", "img24");
            calendario("ctl00_rightbody_S_FEDEC", "img25");
            break;
        case "inicioSolicitud":

            if (document.getElementById("ctl00_rightbody_FechaRecepcion") != null) {
                calendarioConHora("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            }
            if (document.getElementById("ctl00_rightbody_FechaIngresoTramite") != null) {
                calendarioConHora("ctl00_rightbody_FechaIngresoTramite", "imgFechaIngresoTramite");
            }
            if (document.getElementById("ctl00_rightbody_FechaCI") != null) {
                calendarioConHora("ctl00_rightbody_FechaCI", "imgFechaCI");
            }
            break;
        case "inicioSolicitudAmerb":

            if (document.getElementById("ctl00_rightbody_FechaRecepcion") != null) {
                calendarioConHora("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            }
            if (document.getElementById("ctl00_rightbody_FechaIngresoTramite") != null) {
                calendarioConHora("ctl00_rightbody_FechaIngresoTramite", "imgFechaIngresoTramite");
            }
            if (document.getElementById("ctl00_rightbody_FechaCI") != null) {
                calendarioConHoraChange("ctl00_rightbody_FechaCI", "imgFechaCI", "ctl00_rightbody_FechaCI");
            }


            break;
        case "inicioSolicitudColector":
            calendarioConHora("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            break;
        case "agregarActaEntrega":
            calendarioConHora("FechaRecepcion", "imgFechaRecepcion");
            break;
        case "general":
            calendarioConHora("ctl00_rightbody_generalComponente_FechaRecepcion", "imgFechaRecepcion");
            calendarioConHora("ctl00_rightbody_generalComponente_FechaIngresoTramite", "imgFechaIngresoTramite");
            break;
        case "ingresarResoluciones":
            calendario("ctl00_rightbody_Fecha", "ctl00_rightbody_imgFecha");
            calendario("ctl00_rightbody_FechaCI", "ctl00_rightbody_imgFechaCI");
            calendario("ctl00_rightbody_FechaDiarioOficial", "ctl00_rightbody_imgFechaDiarioOficial");
            calendario("ctl00_rightbody_FechaInicioPlazo", "ctl00_rightbody_imgFechaInicioPlazo");
            calendario("ctl00_rightbody_FechaVencimiento", "ctl00_rightbody_imgFechaVencimiento");

            if (document.getElementById("ctl00_rightbody_FechaPrincipal") != null) {
                calendario("ctl00_rightbody_FechaPrincipal", "ctl00_rightbody_imgFechaPrincipal");
            }

            if (document.getElementById("ctl00_rightbody_FechaReferencia") != null) {
                calendario("ctl00_rightbody_FechaReferencia", "ctl00_rightbody_imgFechaReferencia");
            }
            if (document.getElementById("ctl00_rightbody_NuevaFecha") != null) {
                calendario("ctl00_rightbody_NuevaFecha", "ctl00_rightbody_imgNuevaFecha");
            }
            break;

        case "ReporteConcesiones":
            calendario("ctl00_rightbody_FechaDesde", "ctl00_rightbody_imgFechaDesde");
            break;

        case "ingresoInformeRESA":
            calendario("ctl00_rightbody_Fecha", "ctl00_rightbody_imgFecha");
            break;

        case "documentosUnidadEspacial":
            calendario("ctl00_rightbody_documentosUnidadEspacialComponente_Fecha", "ctl00_rightbody_documentosUnidadEspacialComponente_imgFecha");
            calendario("ctl00_rightbody_documentosUnidadEspacialComponente_FechaCI", "ctl00_rightbody_documentosUnidadEspacialComponente_imgFechaCI");
            break;
        case "extensionPlazo":
            calendarioConHora("PlazoInicio", "imgPlazoInicio");

            if (document.getElementById("PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;
        case "unidadEspacial":

            if (document.getElementById("ctl00_rightbody_FechaDiarioOficial") != null) {
                calendarioConHora("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            }
            if (document.getElementById("ctl00_rightbody_FechaActaEntrega") != null) {
                calendarioConHora("ctl00_rightbody_FechaActaEntrega", "imgFechaActaEntrega");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_FechaInicioPeriodo") != null) {
                calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            }

            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }

            break;
        case "unidadEspacialAcopio":
            
            if (document.getElementById("ctl00_rightbody_FechaDiarioOficial") != null) {
                calendarioConHora("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            }
            if (document.getElementById("ctl00_rightbody_FechaActaEntrega") != null) {
                calendarioConHora("ctl00_rightbody_FechaActaEntrega", "imgFechaActaEntrega");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_FechaInicioPeriodo") != null) {
                calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            }
            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;
        case "unidadEspacialAmerb":
            
            if (document.getElementById("ctl00_rightbody_FechaActaEntrega") != null) {
                calendarioConHora("ctl00_rightbody_FechaActaEntrega", "imgFechaActaEntrega");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_FechaInicioPeriodo") != null) {
                calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            }
            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;
        case "unidadEspacialColector":
            
            if (document.getElementById("ctl00_rightbody_FechaActaEntrega") != null) {
                calendarioConHora("ctl00_rightbody_FechaActaEntrega", "imgFechaActaEntrega");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;
        case "unidadEspacialFaenamiento":
            
            if (document.getElementById("ctl00_rightbody_FechaDiarioOficial") != null) {
                calendarioConHora("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            }
            if (document.getElementById("ctl00_rightbody_FechaActaEntrega") != null) {
                calendarioConHora("ctl00_rightbody_FechaActaEntrega", "imgFechaActaEntrega");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_FechaInicioPeriodo") != null) {
                calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            }
            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;
        case "unidadEspacialECMPO":
            
            if (document.getElementById("ctl00_rightbody_FechaDiarioOficial") != null) {
                calendarioConHora("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            }
            if (document.getElementById("ctl00_rightbody_FechaActaEntrega") != null) {
                calendarioConHora("ctl00_rightbody_FechaActaEntrega", "imgFechaActaEntrega");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_FechaInicioPeriodo") != null) {
                calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            }
            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;
        case "unidadEspacialExpAmerb":
            
            if (document.getElementById("ctl00_rightbody_FechaActaEntrega") != null) {
                calendarioConHora("ctl00_rightbody_FechaActaEntrega", "imgFechaActaEntrega");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_FechaInicioPeriodo") != null) {
                calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            }
            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;
        case "unidadEspacialExpConcesion":
            
            if (document.getElementById("ctl00_rightbody_FechaActaEntrega") != null) {
                calendarioConHora("ctl00_rightbody_FechaActaEntrega", "imgFechaActaEntrega");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_FechaInicioPeriodo") != null) {
                calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            }
            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;

        //Modificaciones 
        case "modConcesion":
            calendarioConHora("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            break;
        case "modAcopio":
            calendarioConHora("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            if (document.getElementById("ctl00_rightbody_FechaInicioPeriodo") != null) {
                calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null && document.getElementById("imgPlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;
        case "modAmerb":
            calendarioConHora("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            break;
        case "modColector":

            break;
        case "modFaenamiento":
            calendarioConHora("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            if (document.getElementById("ctl00_rightbody_FechaInicioPeriodo") != null) {
                calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            }
            if (document.getElementById("ctl00_rightbody_PlazoInicio") != null && document.getElementById("imgPlazoInicio") != null) {
                calendarioConHora("ctl00_rightbody_PlazoInicio", "imgPlazoInicio");
            }
            if (document.getElementById("ctl00_rightbody_PlazoVencimiento") != null && document.getElementById("imgPlazoVencimiento") != null) {
                calendarioConHora("ctl00_rightbody_PlazoVencimiento", "imgPlazoVencimiento");
            }
            break;
        case "modECMPO":
            calendarioConHora("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            calendarioConHora("ctl00_rightbody_FechaInicioPeriodo", "imgFechaInicioPeriodo");
            break;



        case "ingresarDocumento_fecha":
            calendario("ctl00_rightbody_Fecha", "imgFecha");
            break;
        case "ingresarDocumento_fechaCI":
            calendario("ctl00_rightbody_FechaCI", "imgFechaCI");
            break;
        case "administrarSolicitudConcesion":
            calendario("ctl00_rightbody_FechaDesde", "imgFechaDesde");
            calendario("ctl00_rightbody_FechaHasta", "imgFechaHasta");
            break;
        case "preIngresarSolicitudRelocalizacion":
            //calendario("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            //calendario("ctl00_rightbody_FechaIngreso", "imgFechaIngreso");
            calendarioConHora("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            calendarioConHora("ctl00_rightbody_FechaIngresoTramite", "imgFechaIngresoTramite");
            break;
            break;
        case "administrarSolicitudRelocalizacion":
            calendario("ctl00_rightbody_FechaIngresoTramiteDesde", "imgFechaDesde");
            calendario("ctl00_rightbody_FechaIngresoTramiteHasta", "imgFechaHasta");
            break;
        case "ingresarSolicitudModificacion":
            //calendario("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            //calendario("ctl00_rightbody_FechaIngresoTramite", "imgFechaIngresoTramite");
            if (document.getElementById("ctl00_rightbody_FechaRecepcion") != null) {
                calendarioConHora("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            }

            if (document.getElementById("ctl00_rightbody_FechaIngresoTramite") != null) {
                calendarioConHora("ctl00_rightbody_FechaIngresoTramite", "imgFechaIngresoTramite");
            }

            if (document.getElementById("ctl00_rightbody_FechaCI") != null) {
                calendarioConHora("ctl00_rightbody_FechaCI", "imgFechaCI");
            }

            break;
        case "administrarSolicitudModificacion":
            calendario("ctl00_rightbody_FechaDesde", "imgFechaDesde");
            calendario("ctl00_rightbody_FechaHasta", "imgFechaHasta");
            break;
        case "unidadEspacialRelocalizacion":
            calendarioConHora("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            //calendario("ctl00_rightbody_FechaDiarioOficial", "imgFechaDiarioOficial");
            break;
        case "unidadEspacialRelocalizacionRESA":
            calendarioConHora("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            break;
        case "administrarSolicitudFaenamiento":
            calendario("ctl00_rightbody_FechaIngresoTramiteDesde", "imgFechaDesde");
            calendario("ctl00_rightbody_FechaIngresoTramiteHasta", "imgFechaHasta");
            break;
        case "administrarSolicitudAmerb":
            calendario("ctl00_rightbody_FechaIngresoTramiteDesde", "imgFechaDesde");
            calendario("ctl00_rightbody_FechaIngresoTramiteHasta", "imgFechaHasta");
            break;
        case "administrarSolicitudColector":
            calendario("ctl00_rightbody_FechaIngresoTramiteDesde", "imgFechaDesde");
            calendario("ctl00_rightbody_FechaIngresoTramiteHasta", "imgFechaHasta");
            break;
        case "administrarSolicitudECMPO":
            calendario("ctl00_rightbody_FechaIngresoTramiteDesde", "imgFechaDesde");
            calendario("ctl00_rightbody_FechaIngresoTramiteHasta", "imgFechaHasta");
            break;
        case "administrarSolicitudExperimentalesAmerb":
            calendario("ctl00_rightbody_FechaIngresoTramiteDesde", "imgFechaDesde");
            calendario("ctl00_rightbody_FechaIngresoTramiteHasta", "imgFechaHasta");
            break;

        case "descansoACS":
            if (document.getElementById("ctl00_rightbody_FechaRecepcion") != null) {
                calendario("ctl00_rightbody_FechaRecepcion", "imgFechaRecepcion");
            }
            if (document.getElementById("ctl00_rightbody_FechaIngresoTramite") != null) {
                calendario("ctl00_rightbody_FechaIngresoTramite", "imgFechaIngresoTramite");
            }
            if (document.getElementById("ctl00_rightbody_FechaInicioProduccion") != null) {
                calendario("ctl00_rightbody_FechaInicioProduccion", "imgFechaInicioProduccion");
            }
            break;


        case "entidadAnalisis":
            calendario("ctl00_rightbody_Fecha", "Img2");
            calendario("ctl00_rightbody_FechaTextRecepcion", "endCal1");
            calendario("ctl00_rightbody_FechaTerminoVigencia", "Img1");
            break;

        case "feriado":
            calendario("ctl00_rightbody_FechaTextRecepcion", "endCal1");
            break;

        case "consultores":
            calendario("ctl00_rightbody_FechaTextRecepcion", "endCal1");
            calendario("ctl00_rightbody_FechaTerminoVigencia", "endCal2");
            break;

        case "proyectoTecnico":
            
            if (document.getElementById("ctl00_rightbody_proyTecnicoComponente_FechaRecepcion") != null) {
                calendarioConHora("ctl00_rightbody_proyTecnicoComponente_FechaRecepcion", "imgFechaRecepcion");
            }
            if (document.getElementById("ctl00_rightbody_proyTecnicoComponente_FechaIngresoTramite") != null) {
                calendarioConHora("ctl00_rightbody_proyTecnicoComponente_FechaIngresoTramite", "imgFechaIngresoTramite");
            }

            break;

    };
}

function Limpiar_Click()
{
    document.getElementById("ctl00_rightbody_FechaDesde").value = "";
    document.getElementById("ctl00_rightbody_FechaHasta").value = "";
}


function CheckAllEmp(Checkbox) {
    var GridVwHeaderChckbox = document.getElementById('ctl00_rightbody_GridVwHeaderChckbox');
    for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
        GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
    }
}
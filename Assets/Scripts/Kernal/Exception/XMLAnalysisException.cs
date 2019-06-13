using System.Collections;
using System.Collections.Generic;
using System;


//自定义一场，XML解析异常  功能：专门定位于XML解析异常，如果出现这个异常，说明XML格式定义错误
public class XMLAnalysisException:Exception{
    public XMLAnalysisException():base(){}

    public XMLAnalysisException(string exceptionMessage):base(exceptionMessage){}
    
}
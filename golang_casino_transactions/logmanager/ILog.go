package logmanager

//
// ILog
//
// interface for log
//
type ILog interface {
    WriteLog(functionName string,step int,str string)
    WriteLogReturn(functionName string,step int,str string,err string)
    WriteLogError(functionName string,step int,str string)
}
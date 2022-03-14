package logmanager

import (
)

// NullLog, a log that don't write, it is used in test, to prevent writes in stdout

type NullLog struct {
}

func (l NullLog)WriteLog(functionName string,step int,str string){
}

func (l NullLog)WriteLogError(functionName string,step int,str string){
}

func (l NullLog)WriteLogReturn(functionName string,step int,str string,err string){
}

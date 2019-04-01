### Hangfire Server以Windows Service形式运行
对外提供```Web API```以操作任务
与[```Quartz.net```](https://github.com/litdev/TimerQuartz)有个使用上的差异是Hangfire添加任务不能指定任务ID，如果任务有取消的情况，则业务中需要存储任务ID

Cron表达式参见这篇 [博客]('https://blog.csdn.net/long870294701/article/details/87983142')
# UnityPublic
## 游戏开发中常用到的公共代码  
### 现有功能：  
1. 定时器： 用于固定时间后调用函数  
2. 事件管理器： 用于发送和接收事件  
3. 热键管理器： 用于将按键和函数调用绑定  
4. ObjectBehaviour：使没有继承MonoBehaviour的类也能拥有类似的生命周期，如 Start, Update, OnDestroy，并能调用协程  
5. Functor：用于将需要多个参数的函数包裹成无参数委托   
6. 单例管理器: 用于需要为单例的类(定义私有构造函数即可)
7. Transform扩展方法FindDownNode：用于在编写UI逻辑时简化子节点查找    


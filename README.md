    by zhanghaijun 710605420@qq.com
    命名规则：
       凡是将要在代码中使用的对象，命名必须以@开头或者包含，对象名必须包含一种unity类型（可不分大小写，但是必须正确）,挂载脚本的对象如果带@ 也会被处理，所以挂载对象不要加@,
    className 为生成的类名，默认为对象名，parentName为父类，默认为MonoBehaviour。目前只生成了C# 脚本，暂时没有生成LUA脚本。可以父节点与子节点同时挂载脚本，但是会生成两份类文件，
    父节点生成文件不包含子类的所有节点，也就是说，子类自动截断节点。
	可自行添加新的类型！！！
    其他命名，请跟进C#语言命名规则。
    如有bug，请提交710605420@qq.com邮箱。
	
![image](https://github.com/hanbim520/Unity3dAutoGenerateCodeForNode/raw/master/Image/1.jpg)
![image](https://github.com/hanbim520/Unity3dAutoGenerateCodeForNode/raw/master/Image/2.jpg)
![image](https://github.com/hanbim520/Unity3dAutoGenerateCodeForNode/raw/master/Image/3.jpg)
![image](https://github.com/hanbim520/Unity3dAutoGenerateCodeForNode/raw/master/Image/4.jpg)

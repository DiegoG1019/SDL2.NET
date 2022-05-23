## Configuring a Window
You can easily configure an SDL Window with all the parameters you want using the class `WindowConfig`

To use it, simply instance a `new object` of type `WindowConfig` and set the appropriate properties or, preferably, chain the setter method calls together.
You can also use `WindowConfig.Default`, you are free to modify it as you wish, and even replace it entirely!

<sub>*Note that a given Window will **not** maintain a reference to the associated WindowConfig, and will be thus eventually garbage collected unless it's set as the `Default` or kept somewhere else in your code.</sub>
<sub>If you don't specify a `WindowConfig` instance, `WindowConfig.Default` will be used instead. These configurations do NOT get added up, they are absolute.</sub>
<sub>Changes to `WindowConfig.Default` will only take effect for newly created Windows. You may want to modify it before launching your Main Window</sub>
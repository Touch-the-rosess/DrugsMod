window.addEventListener('copy', function(e) {
	e.preventDefault();
	SendMessage(g.objectName, g.cutCopyFuncName, 'c');
	event.clipboardData.setData('text/plain', g.clipboardStr);
});
window.addEventListener('paste', function(e) {
	const str = e.clipboardData.getData('text');
	SendMessage("ClientController", "PasteProg", str);
});
window.addEventListener = function(origFn) {
	function noop() {}
	const keys = {'c': true, 'x': true, 'v': true};
	return function(name, fn) {
		const args = Array.prototype.slice.call(arguments);
		if (name !== 'keypress') {
			return origFn.apply(this, args);
		}
		args[1] = function(event) {
			const hArgs = Array.prototype.slice.call(arguments);
			if (keys[event.key.toLowerCase()] && ((event.metaKey ? 1 : 0) + (event.ctrlKey ? 1 : 0)) === 1) {
				event.preventDefault = noop;
			}
			return fn.apply(this, hArgs);
		};
		return origFn.apply(this, args);
	};
}(window.addEventListener);
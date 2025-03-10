mergeInto(LibraryManager.library, {
	CheckSocketIO: function() {
		if (socketIOLoaded) return 1;
		return 0
	},
	CheckSocketIONeedConnect: function() {
		if (socketIOLoaded) return 1;
		return 0
	},
	ConnectSocketIO: function(port, host) {
		console.log("connect socket io");
		var _host = Pointer_stringify(host);
		try {
			if (socket) {
				try {
					socket.disconnect()
				} catch (e) {}
				try {
					socket.socket.disconnect()
				} catch (e) {}
			}
		} catch (e) {}
		socket = io("https://" + _host + ":" + port, {
			reconnection: false
		});
		socket.on("J", (function(eventName, obj) {
				gameInstance.SendMessage("Obvyazka3", "SocketIOJ", eventName + JSON.stringify(obj))
			}
		));
		socket.on("U", (function(eventName, str) {
				gameInstance.SendMessage("Obvyazka3", "SocketIOU", eventName + str)
			}
		));
		socket.on("B", (function(eventName, base64buffer) {
				gameInstance.SendMessage("Obvyazka3", "SocketIOB", eventName + base64buffer)
			}
		));
		socket.on("connect", (function() {
				console.log("connected");
				gameInstance.SendMessage("Obvyazka3", "SocketIOConnected")
			}
		));
		socket.on("disconnect", (function() {
				console.log("disconnected");
				gameInstance.SendMessage("Obvyazka3", "SocketIODisconnected")
			}
		));
		socket.on("connect_error", (function() {
				console.log("connect_error");
				gameInstance.SendMessage("Obvyazka3", "SocketIONoConnect")
            }
		));
	},
	PopupOpenerCaptureClick: function(str) {
		var _url = Pointer_stringify(str);
		console.log("capture click inited " + _url);
		var OpenPopup = (function() {
			window.open(_url, "_blank");
			document.getElementById("gameContainer").removeEventListener("click", OpenPopup)
		});
		console.log("capture element", document.getElementById("gameContainer"));
		document.getElementById("gameContainer").addEventListener("click", OpenPopup, false)
	},
	SendSocketIOB: function(name, buffer) {
		var _name = Pointer_stringify(name);
		var _buffer = Pointer_stringify(buffer);
		socket.emit("B", _name, _buffer)
	},
	SendSocketIOJ: function(name, json) {
		var _name = Pointer_stringify(name);
		var _json = Pointer_stringify(json);
		var jsonObject = JSON.parse(_json);
		socket.emit("J", _name, jsonObject)
	},
	SendSocketIOU: function(name, str) {
		var _name = Pointer_stringify(name);
		var _str = Pointer_stringify(str);
		socket.emit("U", _name, _str)
	},
	SocketIONeedConnect: function() {
		socketNeedConnect = true
	},
	SocketIOPleaseDisconnect: function() {
		socket.disconnect()
	},
	initWebGLCopyAndPaste__postset: '_initWebGLCopyAndPaste();',
	initWebGLCopyAndPaste: function() {
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
		_initWebGLCopyAndPaste = function(objectNamePtr, cutCopyFuncNamePtr, pasteFuncNamePtr) {
			window.becauseUnityIsBadWithJavascript_webglCopyAndPaste =
				window.becauseUnityIsBadWithJavascript_webglCopyAndPaste || {
					initialized: false,
					objectName: Pointer_stringify(objectNamePtr),
					cutCopyFuncName: Pointer_stringify(cutCopyFuncNamePtr),
					pasteFuncName: Pointer_stringify(pasteFuncNamePtr),
				};
				const g = window.becauseUnityIsBadWithJavascript_webglCopyAndPaste;
				if (!g.initialized) {
					window.addEventListener('copy', function(e) {
					e.preventDefault();
					SendMessage("ClientController", "CopyProg", 'c');
					event.clipboardData.setData('text/plain', g.clipboardStr);
				});
				window.addEventListener('paste', function(e) {
					const str = e.clipboardData.getData('text');
					SendMessage("ClientController", "PasteProg", str);
				});
			}
		};
    },
    passCopyToBrowser: function(stringPtr) {
      const g = window.becauseUnityIsBadWithJavascript_webglCopyAndPaste;
      const str = Pointer_stringify(stringPtr);
      g.clipboardStr = str;
    }
});
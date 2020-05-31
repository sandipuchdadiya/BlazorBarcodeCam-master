window.read = function () {

	let reader = null;

	let scanner = null;

	document.getElementById('btn-show-scanner').addEventListener('click', async () => {
		let iptIndex = 0;
		try {

			Dynamsoft.BarcodeScanner.defaultUIElementURL = "/dist/dbr.scanner.customize.html";

			scanner = scanner || await Dynamsoft.BarcodeScanner.createInstance();
			
			await scanner.updateVideoSettings({ video: { width: 700, height: 300, facingMode: "environment" } });
			await scanner.setUIElement(document.getElementById('div-video-container'));


			let scanSettings = await scanner.getScanSettings();
			// disregard duplicated results found in a specified time period
			scanSettings.duplicateForgetTime = 20000;
			// set a scan interval so the library may release the CPU from time to time
			scanSettings.intervalTime = 300;
			await scanner.updateScanSettings(scanSettings);
							
			scanner.onFrameRead = results => {
				if (results.length) {
					console.log(results);
					console.log()
				}
			};
			scanner.onUnduplicatedRead = (txt) => {
				var value = txt;
				var el = document.getElementById('searchText');
				el.value = value;
				el.dispatchEvent(new Event('change'));
				if (1 == ++iptIndex) {
					scanner.onUnduplicatedRead = undefined;
					scanner.hide();
				}

				return value;
			};
			await scanner.show();
		} catch (ex) {
			alert(ex.message);
			throw ex;
		}
	});
}

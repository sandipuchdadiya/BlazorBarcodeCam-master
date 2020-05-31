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


			//let settings = await scanner.getRuntimeSettings();
			//settings.region.regionMeasuredByPercentage = 1;
			//settings.region.regionLeft = 50;
			//settings.region.regionTop = 50;
			//settings.region.regionRight = 75;
			//settings.region.regionBottom = 75;
			//await scanner.updateRuntimeSettings(settings);

		
			// use one of three built-in RuntimeSetting templates, 'speed' is recommended for decoding from a video stream
			//await scanner.updateRuntimeSettings("speed");

			
			// make changes to the template. The code snippet below demonstrates how to specify which symbologies are enabled
			//settings.barcodeFormatIds = Dynamsoft.EnumBarcodeFormat.BF_ONED | Dynamsoft.EnumBarcodeFormat.BF_QR_CODE;
			//await scanner.updateRuntimeSettings(settings);

			// set up the scanner behavior
			
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

const copyMarkdownButtonClass = "pnp-copy-markdown";
const copyMarkdownGroupClass = "pnp-copy-markdown-group";
const copyMarkdownMenuClass = "pnp-copy-markdown-menu";
const markdownSourceCache = new Map();

function getMarkdownSourceUrl() {
	const normalizedPath = window.location.pathname.endsWith("/") ? `${window.location.pathname}index.html` : window.location.pathname;
	const pathSegments = normalizedPath.split("/").filter(Boolean);
	const pageName = pathSegments[pathSegments.length - 1];

	if (!pageName || !/\.html?$/i.test(pageName)) {
		return null;
	}

	const markdownFileName = pageName.replace(/\.html?$/i, ".md");
	const cmdletsIndex = pathSegments.lastIndexOf("cmdlets");
	const articlesIndex = pathSegments.lastIndexOf("articles");
	const sectionIndex = Math.max(cmdletsIndex, articlesIndex);
	let relativeDepth = 0;
	let markdownSegments = ["markdown", markdownFileName];

	if (sectionIndex >= 0) {
		const sectionName = pathSegments[sectionIndex];
		const sectionPathSegments = pathSegments.slice(sectionIndex + 1);
		sectionPathSegments[sectionPathSegments.length - 1] = markdownFileName;
		markdownSegments = ["markdown", sectionName, ...sectionPathSegments];
		relativeDepth = pathSegments.length - sectionIndex - 1;
	}

	return new URL(`${"../".repeat(relativeDepth)}${markdownSegments.join("/")}`, window.location.href);
}

async function getMarkdownSourceText(markdownSourceUrl) {
	const cacheKey = markdownSourceUrl.href;

	if (!markdownSourceCache.has(cacheKey)) {
		markdownSourceCache.set(cacheKey, fetch(markdownSourceUrl, { cache: "no-store" }).then(response => {
			if (!response.ok) {
				throw new Error(`Unable to fetch markdown source: ${response.status}`);
			}

			return response.text();
		}).catch(error => {
			markdownSourceCache.delete(cacheKey);
			throw error;
		}));
	}

	return markdownSourceCache.get(cacheKey);
}

async function writeTextToClipboard(text) {
	if (navigator.clipboard?.writeText) {
		try {
			await navigator.clipboard.writeText(text);
			return;
		}
		catch (error) {
			console.warn("Clipboard API copy failed, falling back to textarea copy.", error);
		}
	}

	const textArea = document.createElement("textarea");
	textArea.value = text;
	textArea.setAttribute("readonly", "");
	textArea.style.position = "fixed";
	textArea.style.top = "0";
	textArea.style.opacity = "0";
	document.body.appendChild(textArea);
	textArea.select();

	try {
		if (!document.execCommand("copy")) {
			throw new Error("Unable to copy markdown source.");
		}
	}
	finally {
		document.body.removeChild(textArea);
	}
}

function setCopyMarkdownButtonState(button, state) {
	const states = {
		ready: { icon: "bi-clipboard", label: "Copy page", disabled: false },
		copying: { icon: "bi-hourglass-split", label: "Copying", disabled: true },
		copied: { icon: "bi-check-lg", label: "Copied", disabled: false },
		failed: { icon: "bi-exclamation-triangle", label: "Copy failed", disabled: false }
	};
	const currentState = states[state] ?? states.ready;
	const icon = document.createElement("i");
	const label = document.createElement("span");

	button.dataset.copyState = state;
	button.disabled = currentState.disabled;
	button.title = currentState.label;
	button.setAttribute("aria-label", currentState.label);
	icon.className = `bi ${currentState.icon}`;
	label.textContent = currentState.label;
	button.replaceChildren(icon, label);
}

function createDropdownItem(elementName, iconClass, label, description, appendedIconClass) {
	const item = document.createElement(elementName);
	const iconWrap = document.createElement("span");
	const icon = document.createElement("i");
	const textWrap = document.createElement("span");
	const labelWrap = document.createElement("span");
	const labelText = document.createElement("span");
	const descriptionText = document.createElement("span");

	item.className = "dropdown-item pnp-copy-markdown-menu-item";
	item.setAttribute("role", "menuitem");
	iconWrap.className = "pnp-copy-markdown-menu-icon";
	icon.className = `bi ${iconClass}`;
	icon.setAttribute("aria-hidden", "true");
	textWrap.className = "pnp-copy-markdown-menu-text";
	labelWrap.className = "pnp-copy-markdown-menu-label";
	labelText.textContent = label;
	descriptionText.className = "pnp-copy-markdown-menu-description";
	descriptionText.textContent = description;

	if (item instanceof HTMLButtonElement) {
		item.type = "button";
	}

	labelWrap.appendChild(labelText);

	if (appendedIconClass) {
		const appendedIcon = document.createElement("i");
		appendedIcon.className = `bi ${appendedIconClass}`;
		appendedIcon.setAttribute("aria-hidden", "true");
		labelWrap.appendChild(appendedIcon);
	}

	iconWrap.appendChild(icon);
	textWrap.append(labelWrap, descriptionText);
	item.append(iconWrap, textWrap);

	return item;
}

function setCopyMarkdownMenuOpen(buttonGroup, toggleButton, toggleIcon, menu, isOpen) {
	buttonGroup.classList.toggle("show", isOpen);
	toggleButton.classList.toggle("show", isOpen);
	menu.classList.toggle("show", isOpen);
	toggleButton.setAttribute("aria-expanded", isOpen.toString());
	toggleIcon.className = `bi ${isOpen ? "bi-chevron-up" : "bi-chevron-down"}`;
}

async function copyMarkdown(markdownSourceText, button) {
	setCopyMarkdownButtonState(button, "copying");

	try {
		await writeTextToClipboard(markdownSourceText);
		setCopyMarkdownButtonState(button, "copied");
		window.setTimeout(() => setCopyMarkdownButtonState(button, "ready"), 1800);
	}
	catch (error) {
		console.error(error);
		setCopyMarkdownButtonState(button, "failed");
		window.setTimeout(() => setCopyMarkdownButtonState(button, "ready"), 2500);
	}
}

async function addCopyMarkdownButton() {
	const actionBar = document.querySelector(".content > .actionbar");
	const markdownSourceUrl = getMarkdownSourceUrl();

	if (!actionBar || !markdownSourceUrl || actionBar.querySelector(`.${copyMarkdownGroupClass}`)) {
		return;
	}

	let markdownSourceText;

	try {
		markdownSourceText = await getMarkdownSourceText(markdownSourceUrl);
	}
	catch (error) {
		console.warn("Markdown source is not available for this page.", error);
		return;
	}

	if (!actionBar.isConnected || actionBar.querySelector(`.${copyMarkdownGroupClass}`)) {
		return;
	}

	const pageActions = document.createElement("div");
	const buttonGroup = document.createElement("div");
	const copyButton = document.createElement("button");
	const toggleButton = document.createElement("button");
	const toggleIcon = document.createElement("i");
	const menu = document.createElement("div");
	const copyMenuItem = createDropdownItem("button", "bi-clipboard", "Copy page", "Copy page as Markdown for LLMs");
	const viewMenuItem = createDropdownItem("a", "bi-markdown", "View as Markdown", "View this page as plain text", "bi-box-arrow-up-right");

	pageActions.className = "pnp-page-actions d-print-none";
	buttonGroup.className = `btn-group ${copyMarkdownGroupClass}`;
	copyButton.type = "button";
	copyButton.className = `btn btn-sm border ${copyMarkdownButtonClass}`;
	toggleButton.type = "button";
	toggleButton.className = "btn btn-sm border pnp-copy-markdown-toggle";
	toggleButton.setAttribute("aria-haspopup", "menu");
	toggleButton.setAttribute("aria-expanded", "false");
	toggleButton.setAttribute("aria-label", "Copy page options");
	toggleButton.title = "Copy page options";
	toggleIcon.className = "bi bi-chevron-down";
	toggleIcon.setAttribute("aria-hidden", "true");
	menu.className = `dropdown-menu dropdown-menu-end ${copyMarkdownMenuClass}`;
	menu.setAttribute("role", "menu");
	viewMenuItem.href = markdownSourceUrl.href;
	viewMenuItem.target = "_blank";
	viewMenuItem.rel = "noopener";

	setCopyMarkdownButtonState(copyButton, "ready");
	copyButton.addEventListener("click", () => copyMarkdown(markdownSourceText, copyButton));
	toggleButton.addEventListener("click", event => {
		event.preventDefault();
		event.stopPropagation();
		setCopyMarkdownMenuOpen(buttonGroup, toggleButton, toggleIcon, menu, !menu.classList.contains("show"));
	});
	copyMenuItem.addEventListener("click", () => {
		setCopyMarkdownMenuOpen(buttonGroup, toggleButton, toggleIcon, menu, false);
		copyMarkdown(markdownSourceText, copyButton);
	});
	viewMenuItem.addEventListener("click", () => setCopyMarkdownMenuOpen(buttonGroup, toggleButton, toggleIcon, menu, false));
	menu.addEventListener("click", event => event.stopPropagation());
	document.addEventListener("click", event => {
		if (!buttonGroup.contains(event.target)) {
			setCopyMarkdownMenuOpen(buttonGroup, toggleButton, toggleIcon, menu, false);
		}
	});
	document.addEventListener("keydown", event => {
		if (event.key === "Escape") {
			setCopyMarkdownMenuOpen(buttonGroup, toggleButton, toggleIcon, menu, false);
		}
	});

	toggleButton.appendChild(toggleIcon);
	menu.append(copyMenuItem, viewMenuItem);
	buttonGroup.append(copyButton, toggleButton, menu);
	pageActions.appendChild(buttonGroup);
	actionBar.appendChild(pageActions);
}

function initializeCopyMarkdownButton() {
	if (document.readyState === "loading") {
		document.addEventListener("DOMContentLoaded", addCopyMarkdownButton, { once: true });
		return;
	}

	addCopyMarkdownButton();
}

initializeCopyMarkdownButton();

export default {
	defaultTheme: "auto",
	start() {
		initializeCopyMarkdownButton();
	}
}
import { readFile, readdir } from 'node:fs/promises';
import { resolve } from 'node:path';

const expectedComponents = [
  "gcds-alert", "gcds-breadcrumbs", "gcds-breadcrumbs-item", "gcds-button", "gcds-card",
  "gcds-checkboxes", "gcds-container", "gcds-date-input", "gcds-date-modified", "gcds-details",
  "gcds-error-message", "gcds-error-summary", "gcds-fieldset", "gcds-file-uploader", "gcds-footer",
  "gcds-grid", "gcds-grid-col", "gcds-header", "gcds-heading", "gcds-hint", "gcds-icon", "gcds-input",
  "gcds-label", "gcds-lang-toggle", "gcds-link", "gcds-nav-group", "gcds-nav-link", "gcds-notice",
  "gcds-pagination", "gcds-radios", "gcds-search", "gcds-select", "gcds-side-nav", "gcds-signature",
  "gcds-sr-only", "gcds-stepper", "gcds-table", "gcds-text", "gcds-textarea", "gcds-top-nav", "gcds-topic-menu"
];

const expectedEvents = [
  "gcdsDismiss", "gcdsClick", "gcdsFocus", "gcdsBlur", "gcdsInput", "gcdsChange",
  "gcdsError", "gcdsValid", "gcdsSubmit", "gcdsRemoveFile", "gcdsSuggestionSelected", "gcdsTableStateChange"
];

const projectRoot = resolve(import.meta.dirname, '..');
const generatedDir = resolve(projectRoot, 'Components/Generated');
const files = (await readdir(generatedDir)).filter(x => x.endsWith('.cs'));
const wrapperText = (await Promise.all(files.map(f => readFile(resolve(generatedDir, f), 'utf8')))).join('\n');

const missingComponents = expectedComponents.filter(tag => !wrapperText.includes(`=> "${tag}"`));
if (missingComponents.length) {
  console.error('Missing wrappers:', missingComponents.join(', '));
  process.exit(1);
}
if (files.length !== expectedComponents.length) {
  console.error(`Expected ${expectedComponents.length} generated wrappers, found ${files.length}.`);
  process.exit(1);
}

const interopText = await readFile(resolve(projectRoot, 'Interop/GcdsInterop.cs'), 'utf8');
const missingEvents = expectedEvents.filter(name => !interopText.includes(`"${name}"`));
if (missingEvents.length) {
  console.error('Missing GCDS event registrations:', missingEvents.join(', '));
  process.exit(1);
}

const pkg = JSON.parse(await readFile(resolve(projectRoot, 'package.json'), 'utf8'));
if (pkg.dependencies?.['@gcds-core/components'] !== '1.4.0') {
  console.error('This compatibility baseline expects @gcds-core/components 1.4.0.');
  process.exit(1);
}

console.log(`Wrapper coverage OK: ${expectedComponents.length}/${expectedComponents.length} GCDS components.`);
console.log(`Custom event coverage OK: ${expectedEvents.length}/${expectedEvents.length} released event names.`);

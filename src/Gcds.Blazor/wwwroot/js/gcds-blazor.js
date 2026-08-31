const registrations = new WeakMap();

export async function configure(element, properties, dotNetRef, eventNames) {
  if (!element) return;
  const tag = element.tagName?.toLowerCase();
  if (tag && globalThis.customElements?.whenDefined) {
    try { await globalThis.customElements.whenDefined(tag); } catch { /* no-op */ }
  }
  if (properties) {
    for (const [key, value] of Object.entries(properties)) {
      if (value !== undefined) element[key] = value;
    }
  }
  detach(element);
  const listeners = new Map();
  for (const name of eventNames ?? []) {
    const listener = event => {
      const detail = event.detail === undefined ? null : event.detail;
      dotNetRef.invokeMethodAsync('HandleGcdsEventAsync', name, detail);
    };
    element.addEventListener(name, listener);
    listeners.set(name, listener);
  }
  registrations.set(element, listeners);
}

export function detach(element) {
  const listeners = registrations.get(element);
  if (!listeners) return;
  for (const [name, listener] of listeners.entries()) element.removeEventListener(name, listener);
  registrations.delete(element);
}

export async function invoke(element, methodName, args) {
  if (!element) throw new Error('GCDS element is not available.');
  const tag = element.tagName?.toLowerCase();
  if (tag && globalThis.customElements?.whenDefined) await globalThis.customElements.whenDefined(tag);
  const method = element[methodName];
  if (typeof method !== 'function') throw new Error(`${methodName} is not a function on ${element.tagName}.`);
  return await method.apply(element, args ?? []);
}

export function getProperty(element, propertyName) {
  return element?.[propertyName];
}

export function setProperty(element, propertyName, value) {
  if (!element) throw new Error('GCDS element is not available.');
  element[propertyName] = value;
}

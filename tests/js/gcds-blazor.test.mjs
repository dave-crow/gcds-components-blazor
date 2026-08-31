import test from 'node:test';
import assert from 'node:assert/strict';
import { configure, detach, invoke, getProperty, setProperty } from '../../src/Gcds.Blazor/wwwroot/js/gcds-blazor.js';

globalThis.customElements = { whenDefined: async () => {} };
class FakeElement {
  constructor(){ this.tagName='GCDS-INPUT'; this.listeners=new Map(); }
  addEventListener(n,f){ this.listeners.set(n,f); }
  removeEventListener(n){ this.listeners.delete(n); }
}

test('configure sets complex properties and forwards custom events', async () => {
  const el = new FakeElement();
  const calls=[]; const dotnet={ invokeMethodAsync: async (...a)=>calls.push(a) };
  await configure(el, { options:[{id:'a'}], value:'x' }, dotnet, ['gcdsChange', 'gcdsSuggestionSelected']);
  assert.deepEqual(el.options,[{id:'a'}]); assert.equal(el.value,'x');
  el.listeners.get('gcdsChange')({detail:'y'});
  el.listeners.get('gcdsSuggestionSelected')({detail:'z'});
  await new Promise(r=>setImmediate(r));
  assert.deepEqual(calls[0], ['HandleGcdsEventAsync','gcdsChange','y']);
  assert.deepEqual(calls[1], ['HandleGcdsEventAsync','gcdsSuggestionSelected','z']);
  detach(el); assert.equal(el.listeners.size,0);
});

test('invoke calls web component methods', async () => {
  const el = new FakeElement(); el.checkValidity=async()=>true;
  assert.equal(await invoke(el,'checkValidity',[]),true);
});

test('property helpers read and write web component properties', () => {
  const el = new FakeElement();
  setProperty(el, 'answer', 42);
  assert.equal(getProperty(el,'answer'),42);
});

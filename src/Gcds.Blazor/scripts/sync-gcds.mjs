import { cp, mkdir, rm, access } from 'node:fs/promises';
import { resolve } from 'node:path';

const projectRoot = resolve(import.meta.dirname, '..');
const source = resolve(projectRoot, 'node_modules/@gcds-core/components/dist/gcds');
const target = resolve(projectRoot, 'wwwroot/gcds');

try { await access(source); }
catch { throw new Error(`GCDS assets not found at ${source}. Run npm install first.`); }
await rm(target, { recursive: true, force: true });
await mkdir(target, { recursive: true });
await cp(source, target, { recursive: true });
console.log(`Synced @gcds-core/components assets to ${target}`);

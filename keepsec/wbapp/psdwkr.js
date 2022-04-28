function HieName(pa,sonName)
{
	var cleanSon=sonName.replaceAll('/','^');
	if(!pa){return cleanSon;}
	var panai= pa.name;
	if(panai=='ROOT'){return cleanSon;}

	return panai+'/'+cleanSon;
}


var e, t;
! function(e) {
	e.SectionDividerSetting = "lsct",
		e.TypeToolObjectSetting = "TySh",
		e.UnicodeLayerName = "luni"
}(e || (e = {})),
function(e) {
	e[e.Normal = 0] = "Normal",
		e[e.SceneGroup = 1] = "SceneGroup"
}(t || (t = {}));
const r = (e, t) => {
	const r = Math.max(e.length, t.length);
	for (let n = 0; n < r; n++)
		if (e[n] !== t[n])
			return !1;
	return !0
};
class n extends Error {
	constructor(e) {
		super(e),
			Object.setPrototypeOf(this, new.target.prototype),
			this.name = new.target.name
	}
}
class a extends n {}
class i extends n {}
class s extends n {}
class o extends n {}
class c extends n {}
class d extends n {}
class l extends n {}
class u extends n {}
class h extends n {}
class p extends n {}
class g extends n {}
class f extends n {}
class y extends n {}
class w extends n {}
class m extends n {}
class S extends n {}
class b extends n {}
class v extends n {}
class x extends n {}
class I extends n {}
class L extends n {}
class k extends n {}
class D extends n {}
class T extends n {}
class M extends n {}
class U extends n {}
class R extends n {}
class C extends n {}
class P extends n {}
class F extends n {}
class O extends n {}
class G extends n {}
class B extends n {}
class A extends n {}
class E extends n {}
class z extends n {}

function V(e, t) {
	const r = e.getUint32(t),
		n = e.getUint32(t + 4);
	if (r >= 2097152)
		throw new U;
	return 4294967296 * r + n
}
const $ = {
	u8: 1,
	i8: 1,
	u16: 2,
	i16: 2,
	u32: 4,
	i32: 4,
	u64: 8,
	i64: 8,
	f64: 8
};
class N {
	constructor(e, t = 0) {
		this.dataView = e,
			this.position = t
	}
	get length() {
		return this.dataView.byteLength
	}
	clone(e) {
		const t = void 0 !== e ? e : this.position;
		return new N(new DataView(this.dataView.buffer, this.dataView.byteOffset, this.dataView.byteLength), t)
	}
	pass(e) {
		this.position += e
	}
	extract(e) {
		if (this.position + e > this.dataView.byteLength)
			throw new I;
		return new Uint8Array(this.dataView.buffer, this.dataView.byteOffset + this.position, e)
	}
	take(e) {
		const t = this.extract(e);
		return this.pass(e),
			t
	}
	read(e) {
		const {
			dataView: t,
			position: r
		} = this;
		switch (this.pass($[e]),
			e) {
			case "u8":
				return t.getUint8(r);
			case "u16":
				return t.getUint16(r);
			case "u32":
				return t.getUint32(r);
			case "u64":
				return V(t, r);
			case "i8":
				return t.getInt8(r);
			case "i16":
				return t.getInt16(r);
			case "i32":
				return t.getInt32(r);
			case "i64":
				return function(e, t) {
					const r = e.getInt32(t),
						n = e.getUint32(t + 4);
					if (r >= 2097152 || r < -2097152 || -2097152 === r && 0 === n)
						throw new U;
					return 4294967296 * r + n
				}(t, r);
			case "f64":
				return t.getFloat64(r);
			default:
				throw new TypeError(`Invalid ReadType: ${e}`)
		}
	}
	readString(e) {
		const t = this.take(e);
		return (new TextDecoder).decode(t)
	}
	readUnicodeString(e = 4) {
		const t = 2 * this.read("u32"),
			r = this.take(t),
			n = new TextDecoder("utf-16be").decode(r);
		return this.padding(4 + t, e),
			0 === n.charCodeAt(n.length - 1) ? n.slice(0, -1) : n
	}
	readIdString() {
		const e = this.read("u32");
		return this.readString(e || 4)
	}
	padding(e, t) {
		const r = e % t;
		r > 0 && this.pass(t - r)
	}
}
const j = (e, t, r) => t <= e && e <= r;
var H, X, Y, W, Z, _, q;

function J(e) {
	const t = Object.keys(H);
	for (const r of t)
		if (H[r] === e)
			return e;
	throw new p
}

function K(e, t, r) {
	const n = e.items.get(t);
	if (!n)
		throw new B(`Cannot find key "${t}" in descriptor`);
	if (n.type !== r)
		throw new A(`Unexpected descriptor value type: expected "${r}" but got "${n.type}"`);
	return n
}! function(e) {
	e.PassThrough = "pass",
		e.Normal = "norm",
		e.Dissolve = "diss",
		e.Darken = "dark",
		e.Multiply = "mul ",
		e.ColorBurn = "idiv",
		e.LinearBurn = "lbrn",
		e.DarkerColor = "dkCl",
		e.Lighten = "lite",
		e.Screen = "scrn",
		e.ColorDodge = "div ",
		e.LinearDodge = "lddg",
		e.LighterColor = "lgCl",
		e.Overlay = "over",
		e.SoftLight = "sLit",
		e.HardLight = "hLit",
		e.VividLight = "vLit",
		e.LinearLight = "lLit",
		e.PinLight = "pLit",
		e.HardMix = "hMix",
		e.Difference = "diff",
		e.Exclusion = "smud",
		e.Subtract = "fsub",
		e.Divide = "fdiv",
		e.Hue = "hue ",
		e.Saturation = "sat ",
		e.Color = "colr",
		e.Luminosity = "lum "
}(H || (H = {})),
function(e) {
	e[e.Base = 0] = "Base",
		e[e.NonBase = 1] = "NonBase"
}(X || (X = {})),
function(e) {
	e[e.Bitmap = 0] = "Bitmap",
		e[e.Grayscale = 1] = "Grayscale",
		e[e.Indexed = 2] = "Indexed",
		e[e.Rgb = 3] = "Rgb",
		e[e.Cmyk = 4] = "Cmyk",
		e[e.Multichannel = 7] = "Multichannel",
		e[e.Duotone = 8] = "Duotone",
		e[e.Lab = 9] = "Lab"
}(Y || (Y = {})),
function(e) {
	e[e.One = 1] = "One",
		e[e.Eight = 8] = "Eight",
		e[e.Sixteen = 16] = "Sixteen",
		e[e.ThirtyTwo = 32] = "ThirtyTwo"
}(W || (W = {})),
function(e) {
	e.Alias = "alis",
		e.Boolean = "bool",
		e.Class = "type",
		e.Descriptor = "Objc",
		e.Double = "doub",
		e.Enumerated = "enum",
		e.GlobalClass = "GlbC",
		e.GlobalObject = "GlbO",
		e.Integer = "long",
		e.LargeInteger = "comp",
		e.List = "VlLs",
		e.RawData = "tdta",
		e.Reference = "obj ",
		e.String = "TEXT",
		e.UnitFloat = "UntF"
}(Z || (Z = {})),
function(e) {
	e.Angle = "#Ang",
		e.Density = "#Rsl",
		e.Distance = "#Rlt",
		e.Millimeters = "#Mlm",
		e.None = "#Nne",
		e.Percent = "#Prc",
		e.Pixels = "#Pxl",
		e.Points = "#Pnt"
}(_ || (_ = {})),
function(e) {
	e[e.PSD = 1] = "PSD",
		e[e.PSB = 2] = "PSB"
}(q || (q = {}));
const Q = [56, 66, 80, 83],
	ee = [0, 0, 0, 0, 0, 0],
	te = [1, 8, 16, 32];

function re(e, t, r, n, a) {
	const i = new N(e),
		s = i.read("u16");
	if (!(s in we))
		throw new g;
	const {
		red: o,
		green: c,
		blue: d,
		alpha: l
	} = (() => {
		switch (s) {
			case we.RawData:
				return function(e, t, r) {
					const n = (e.length - 2) / r,
						a = e.extract(n),
						i = r >= 2 ? e.extract(n) : void 0,
						s = r >= 3 ? e.extract(n) : void 0,
						o = r >= 4 ? e.extract(n) : void 0;
					if (t === W.Eight)
						return {
							red: a,
							green: i,
							blue: s,
							alpha: o
						};
					throw new y(`Unsupported image bit depth: ${t}`)
				}(i, t, n);
			case we.RleCompressed:
				return function(e, t, r, n) {
					let a = 0,
						i = 0,
						s = 0,
						o = 0;
					const c = n.rleScanlineLengthFieldReadType;
					for (let t = 0; t < r; t++)
						a += e.read(c);
					if (t >= 2)
						for (let t = 0; t < r; t++)
							i += e.read(c);
					if (t >= 3)
						for (let t = 0; t < r; t++)
							s += e.read(c);
					if (4 === t)
						for (let t = 0; t < r; t++)
							o += e.read(c);
					const d = 2 + t * r * n.rleScanlineLengthFieldSize,
						l = e.clone(d);
					return {
						red: l.take(a),
						green: i ? l.take(i) : void 0,
						blue: s ? l.take(s) : void 0,
						alpha: o ? l.take(o) : void 0
					}
				}(i, n, r, a);
			default:
				throw new f
		}
	})();
	return {
		compression: s,
		red: o,
		green: c,
		blue: d,
		alpha: l
	}
}
const ne = [56, 66, 73, 77];

function ae(e) {
	const t = e.take(4);
	if (!r(t, ne))
		throw new x;
	const n = e.read("i16"),
		a = function(e, t = 0) {
			const r = e.read("u8"),
				n = e.readString(r);
			if (t) {
				const n = (r + 1) % t;
				n > 0 && e.pass(t - n)
			}
			return n
		}(e, 2),
		i = e.read("u32"),
		s = e.position + i,
		o = i + i % 2,
		c = e.position;
	let d = null;
	switch (n) {
		case ve.GridAndGuides:
			d = function(e) {
				const t = e.read("u32");
				if (1 !== t)
					throw new L;
				const r = e.read("u32"),
					n = e.read("u32"),
					a = e.read("u32"),
					i = [];
				for (let t = 0; t < a; ++t) {
					const t = e.read("i32"),
						r = De(e.read("u8"));
					i.push({
						position: t,
						direction: r
					})
				}
				return {
					version: t,
					gridSizeX: r,
					gridSizeY: n,
					guides: i
				}
			}(e);
			break;
		case ve.Slices:
			d = function(e, t) {
				const r = e.read("u32");
				if (6 === r) {
					const n = e.read("i32"),
						a = e.read("i32"),
						i = e.read("i32"),
						s = e.read("i32"),
						o = e.readUnicodeString(0),
						c = e.read("u32"),
						d = [];
					for (; d.length < c;) {
						const t = e.read("u32"),
							r = e.read("u32"),
							n = Te(e.read("u32")),
							a = 1 === n ? e.read("u32") : void 0;
						d.push({
							id: t,
							groupId: r,
							origin: n,
							associatedLayerId: a,
							name: e.readUnicodeString(0),
							type: e.read("u32"),
							left: e.read("i32"),
							top: e.read("i32"),
							right: e.read("i32"),
							bottom: e.read("i32"),
							url: e.readUnicodeString(0),
							target: e.readUnicodeString(0),
							message: e.readUnicodeString(0),
							altTag: e.readUnicodeString(0),
							isCellTextHtml: Boolean(e.read("u8")),
							cellText: e.readUnicodeString(0),
							horizontalAlignment: e.read("i32"),
							verticalAlignment: e.read("i32"),
							alpha: e.read("u8"),
							red: e.read("u8"),
							green: e.read("u8"),
							blue: e.read("u8")
						})
					}
					return {
						version: r,
						boundTop: n,
						boundLeft: a,
						boundBottom: i,
						boundRight: s,
						sliceGroupName: o,
						slices: d,
						descriptor: e.position < t ? Pe(e) : void 0
					}
				}
				if (7 === r || 8 === r)
					return {
						version: r,
						descriptor: Pe(e)
					};
				throw new D(`Invalid Slices section version: ${r}`)
			}(e, s)
	}
	const l = c + o - e.position;
	return l > 0 && e.pass(l), {
		id: n,
		name: a,
		resource: d
	}
}
const ie = (e, t, r) => {
	const {
		top: n,
		left: a,
		bottom: i,
		right: s,
		opacity: o,
		clipping: c,
		visible: d,
		blendMode: l,
		layerText: u
	} = t;
	return {
		name: e,
		top: n,
		left: a,
		bottom: i,
		right: s,
		opacity: o,
		clippingMask: c,
		visible: d,
		blendMode: l,
		groupId: r,
		text: u
	}
};
class se {
	constructor(e, t) {
		this.channels = e,
			this.layerProperties = t
	}
	static create(e, t, r) {
		const n = ie(e.name, e, r);
		return new se(t, n)
	}
	get red() {
		const e = this.channels.get(me.Red);
		if (void 0 === e)
			throw new m;
		return e
	}
	get green() {
		return this.channels.get(me.Green)
	}
	get blue() {
		return this.channels.get(me.Blue)
	}
	get alpha() {
		return this.channels.get(me.TransparencyMask)
	}
	get width() {
		const {
			right: e,
			left: t
		} = this.layerProperties;
		return e - t + 1
	}
	get height() {
		const {
			bottom: e,
			top: t
		} = this.layerProperties;
		return e - t + 1
	}
}
class oe {
	constructor(e, t) {
		this.id = e,
			this.layerProperties = t
	}
	static create(e, t, r, n) {
		const a = ie(e, r, n);
		return new oe(t, a)
	}
}

function ce(e, r) {
	const n = function(e) {
		if (e in Se)
			return e;
		throw new h
	}(e.read("u32"));
	if (r < 12)
		return {
			dividerType: n
		};
	const a = e.readString(4);
	if ("8BIM" !== a)
		throw new C(`Invalid Section Divider Setting signature: ${a}`);
	const i = J(e.readString(4));
	if (r < 16)
		return {
			dividerType: n,
			dividerSignature: a,
			blendMode: i
		};
	const s = e.read("u32");
	if (!(s in t))
		throw new C(`Invalid Section Divider Setting subtype: ${s}`);
	return {
		dividerType: n,
		dividerSignature: a,
		blendMode: i,
		subType: s
	}
}

function de(e) {
	const t = e.read("u16");
	if (1 !== t)
		throw new P(`Invalid type tool object setting version: ${t}`);
	const r = e.read("f64"),
		n = e.read("f64"),
		a = e.read("f64"),
		i = e.read("f64"),
		s = e.read("f64"),
		o = e.read("f64"),
		c = e.read("u16");
	if (50 !== c)
		throw new P(`Invalid text version: ${c}`);
	const d = Pe(e),
		l = e.read("u16");
	if (1 !== l)
		throw new P(`Invalid warp version: ${l}`);
	return {
		version: t,
		transformXX: r,
		transformXY: n,
		transformYX: a,
		transformYY: i,
		transformTX: s,
		transformTY: o,
		textVersion: c,
		textData: d,
		warpVersion: l,
		warpData: Pe(e),
		left: e.read("f64"),
		top: e.read("f64"),
		right: e.read("f64"),
		bottom: e.read("f64")
	}
}

function le(e) {
	return {
		name: e.readUnicodeString(0)
	}
}

function ue(t, r) {
	const n = t.readString(4);
	if ("8BIM" !== n && "8B64" !== n)
		throw new R(`Invalid signature: ${n}`);
	const a = t.readString(4),
		i = t.read(function(e, t) {
			if (t.aliLengthFieldSizeIsVariable)
				switch (e) {
					case "LMsk":
					case "Lr16":
					case "Lr32":
					case "Layr":
					case "Mt16":
					case "Mt32":
					case "Mtrn":
					case "Alph":
					case "FMsk":
					case "Ink2":
					case "FEid":
					case "FXid":
					case "PxSD":
						return "u64"
				}
			return "u32"
		}(a, r)),
		s = t.position,
		o = function(t, r, n, a) {
			switch (n) {
				case e.SectionDividerSetting:
					return {
						signature: r,
							key: n,
							...ce(t, a)
					};
				case e.TypeToolObjectSetting:
					return {
						signature: r,
							key: n,
							...de(t)
					};
				case e.UnicodeLayerName:
					return {
						signature: r,
							key: n,
							...le(t)
					};
				default:
					return {
						signature: r,
							key: n,
							_isUnknown: !0,
							data: t.take(a)
					}
			}
		}(t, n, a, i),
		c = i - (t.position - s);
	return t.pass(c),
		t.padding(i, 4),
		o
}

function he(t, r) {
	const [n, a, i, s] = function(e) {
		const t = e.read("i32"),
			r = e.read("i32");
		let n = e.read("i32");
		0 !== n && (n -= 1);
		let a = e.read("i32");
		return 0 !== a && (a -= 1),
			[t, r, n, a]
	}(t), o = t.read("u16"), c = [];
	for (; c.length < o;) {
		const e = t.read("i16"),
			n = t.read(r.layerRecordSectionChannelLengthFieldReadType) - 2;
		c.push([e, n])
	}
	if ("8BIM" !== t.readString(4))
		throw new u;
	const d = J(t.readString(4)),
		l = t.read("u8"),
		h = function(e) {
			if (e === X.Base)
				return X.Base;
			if (e === X.NonBase)
				return X.NonBase;
			throw new S
		}(t.read("u8")),
		p = function(e) {
			return  e.read("u8")
		}(t);
	t.pass(1);
	const g = t.read("u32"),
		f = t.position;
	t.pass(t.read("u32")),
		t.pass(t.read("u32"));
	const y = t.read("u8");
	let w = t.readString(y);
	t.padding(y + 1, 4);
	const m = [];
	for (; t.position - f < g;)
		m.push(ue(t, r));
	let b, v;
	for (const t of m)
		if (!t._isUnknown)
			switch (t.key) {
				case e.SectionDividerSetting:
					({
						dividerType: b
					} = t);
					break;
				case e.TypeToolObjectSetting: {
					const e = t.textData.descriptor.items.get("Txt ");
					e && e.type === Z.String && (v = e.value);
					break
				}
				case e.UnicodeLayerName:
					({
						name: w
					} = t)
			}
	return {
		name: w,
		channelInformation: c,
		top: n,
		left: a,
		bottom: i,
		right: s,
		visible: p,
		opacity: l,
		clipping: h,
		blendMode: d,
		additionalLayerInfos: m,
		dividerType: b,
		layerText: v
	}
}

function pe(e, t, r = 4) {
	const n = (4 === r ? e.getUint32(t) : V(e, t)) + r;
	return {
		start: t,
		end: t + n,
		size: n
	}
}
const ge = {
		maxPixels: 3e4,
		rleScanlineLengthFieldSize: 2,
		rleScanlineLengthFieldReadType: "u16",
		layerAndMaskSectionLengthFieldSize: 4,
		layerInfoSectionLengthFieldSize: 4,
		layerRecordSectionChannelLengthFieldReadType: "u32",
		aliLengthFieldSizeIsVariable: !1
	},
	fe = {
		maxPixels: 3e5,
		rleScanlineLengthFieldSize: 4,
		rleScanlineLengthFieldReadType: "u32",
		layerAndMaskSectionLengthFieldSize: 8,
		layerInfoSectionLengthFieldSize: 8,
		layerRecordSectionChannelLengthFieldReadType: "u64",
		aliLengthFieldSizeIsVariable: !0
	};

function ye(e) {
	switch (e) {
		case q.PSD:
			return ge;
		case q.PSB:
			return fe;
		default:
			throw new i
	}
}
var we, me, Se, be, ve, xe, Ie;

function Le(e) {
	if (!(e in we))
		throw new g;
	return e
}

function ke(e) {
	switch (e) {
		case me.Red:
			return 0;
		case me.Green:
			return 1;
		case me.Blue:
			return 2;
		case me.TransparencyMask:
			return 3;
		default:
			throw new w
	}
}

function De(e) {
	if (!(e in be))
		throw new k;
	return e
}

function Te(e) {
	if (!(e in xe))
		throw new T(`Invalid slice origin: ${e}`);
	return e
}

function Me(e, t = 255) {
	if (!(0 <= t && t <= 255))
		throw new v;
	const r = e.length / 4,
		n = ke(me.TransparencyMask),
		a = t / 255;
	for (let t = 0; t < r; t++) {
		const r = 4 * t + n;
		e[r] = Math.floor(a * e[r])
	}
	return e
}

function Ue(e, t, r, n, a) {
	try {
		t.set(n.data)
	} catch (e) {
		throw e instanceof RangeError ? new Error(`Channel data (${n.data.byteLength} bytes) is too large for the input region (${t.byteLength} bytes)`) : e
	}
	let i;
	switch (n.compression) {
		case we.RawData:
			i = e.decodeUncompressedChannel(t.byteOffset, n.data.byteLength, r, a);
			break;
		case we.RleCompressed:
			i = e.decodeRleChannel(t.byteOffset, n.data.byteLength, r, a);
			break;
		default:
			throw new f(`Unsupported compression method: ${n.compression}`)
	}
	if (0 !== i)
		throw new Error(`Decode failed (cause: ${i})`)
}

function Re(e) {
	const t = function(e) {
			const t = new DataView(e),
				n = function(e) {
					const t = new N(e),
						n = t.take(4);
					if (!r(n, Q))
						throw new a;
					const u = t.read("u16");
					if (u !== q.PSD && u !== q.PSB)
						throw new i;
					const h = ye(u),
						p = t.take(6);
					if (!r(p, ee))
						throw new s;
					const g = t.read("u16");
					if (!j(g, 1, 56))
						throw new d;
					const f = t.read("u32"),
						y = t.read("u32");
					if (!j(f, 1, h.maxPixels) || !j(y, 1, h.maxPixels))
						throw new l;
					const w = t.read("u16");
					if (!te.includes(w))
						throw new c;
					const m = t.read("u16");
					if (m in Y == 0)
						throw new o;
					return {
						channelCount: g,
						version: u,
						width: y,
						height: f,
						depth: w,
						colorMode: m
					}
				}(new DataView(e, 0, 26)),
				u = ye(n.version),
				h = pe(t, 26),
				p = pe(t, h.end),
				g = pe(t, p.end, u.layerAndMaskSectionLengthFieldSize);
			return {
				fileHeader: n,
				colorModeData: new DataView(e, h.size),
				imageResources: new DataView(e, p.start, p.size),
				layerAndMaskInformation: new DataView(e, g.start, g.size),
				imageData: new DataView(e, g.end)
			}
		}(e),
		{
			fileHeader: n
		} = t,
		u = ye(n.version),
		h = function(e) {
			const t = new N(e),
				r = [],
				n = t.read("u32");
			for (; t.position < n;) {
				const e = ae(t);
				r.push(e)
			}
			return {
				resources: r
			}
		}(t.imageResources),
		p = function(e, t) {
			const r = new N(e);
			r.pass(t.layerAndMaskSectionLengthFieldSize),
				r.pass(t.layerInfoSectionLengthFieldSize);
			const n = r.read("i16"),
				a = Math.abs(n),
				i = function(e, t, r) {
					const n = [];
					for (; n.length < t;)
						n.push(he(e, r));
					const a = n.map((t => {
						const n = function(e) {
								return e.bottom - e.top + 1
							}(t),
							a = function(e, t, r, n) {
								const a = new Map,
									{
										length: i
									} = t;
								for (let s = 0; s < i; s++) {
									const [i, o] = t[s], c = Le(e.read("u16")), d = e.take(o);
									switch (c) {
										case we.RawData:
											a.set(i, {
												compression: c,
												data: d
											});
											break;
										case we.RleCompressed: {
											const e = r * n.rleScanlineLengthFieldSize,
												t = new Uint8Array(d.buffer, d.byteOffset + e, d.byteLength - e);
											a.set(i, {
												compression: c,
												data: t
											});
											break
										}
									}
								}
								return a
							}(e, t.channelInformation, n, r);
						return [t, a]
					})).reverse();
					return a
				}(r, a, t),
				s = [],
				o = [],
				c = [],
				d = [{
					startIndex: 0,
					groupId: 0,
					parentGroupId: 0
				}];
			let l = 0;
			for (let e = 0; e < a; e++) {
				const [t, r] = i[e], n = d[d.length - 1].groupId, {
					dividerType: a
				} = t;
				if (a === Se.CloseFolder || a === Se.OpenFolder)
					l += 1,
					d.push({
						startIndex: s.length,
						groupId: l,
						parentGroupId: n,
						layerRecord: t
					}),
					c.push("G");
				else if (a === Se.BoundingSection) {
					const e = d.pop();
					if (void 0 === e)
						throw new b;
					const r = e.groupId > 0 ? e.groupId : void 0,
						n = e.layerRecord || t;
					o.push(oe.create(n.name, e.groupId, n, r)),
						c.push("D")
				} else
					s.push(se.create(t, r, n)),
					c.push("L")
			}
			return o.sort(((e, t) => e.id - t.id)), {
				layers: s,
				groups: o,
				orders: c
			}
		}(t.layerAndMaskInformation, u);
	return {
		fileHeader: n,
		colorModeData: void 0,
		imageResources: h,
		layerAndMaskInfo: p,
		imageData: re(t.imageData, n.depth, n.height, n.channelCount, u)
	}
}

function Ce(e) {
	const t = e.readUnicodeString(0),
		r = e.readIdString(),
		n = e.read("u32"),
		a = new Map;
	for (; a.size < n;) {
		const t = e.readIdString(),
			r = Fe(e);
		if (a.has(t))
			throw new G(`Duplicate descriptor key: ${t}`);
		a.set(t, r)
	}
	return {
		name: t,
		classId: r,
		items: a
	}
}

function Pe(e) {
	const t = e.read("u32");
	if (16 !== t)
		throw new O(`Invalid descriptor version: ${t}`);
	return {
		descriptorVersion: t,
		descriptor: Ce(e)
	}
}

function Fe(e) {
	const t = e.readString(4);
	switch (t) {
		case Z.Alias: {
			const r = e.read("u32");
			return {
				type: t,
				data: e.take(r)
			}
		}
		case Z.Boolean:
			return {
				type: t,
					value: Boolean(e.read("u8"))
			};
		case Z.Class:
		case Z.GlobalClass:
			return {
				type: t,
					name: e.readUnicodeString(0),
					classId: e.readIdString()
			};
		case Z.Descriptor:
		case Z.GlobalObject:
			return {
				type: t,
					descriptor: Ce(e)
			};
		case Z.Double:
			return {
				type: t,
					value: e.read("f64")
			};
		case Z.Enumerated:
			return {
				type: t,
					enumType: e.readIdString(),
					enumValue: e.readIdString()
			};
		case Z.Integer:
			return {
				type: t,
					value: e.read("i32")
			};
		case Z.LargeInteger:
			return {
				type: t,
					value: e.read("i64")
			};
		case Z.List: {
			const r = e.read("u32"),
				n = [];
			for (; n.length < r;)
				n.push(Fe(e));
			return {
				type: t,
				values: n
			}
		}
		case Z.RawData: {
			const r = e.read("u32");
			return {
				type: t,
				data: e.take(r)
			}
		}
		case Z.Reference: {
			const r = e.read("u32"),
				n = [];
			for (; n.length < r;)
				n.push(Oe(e));
			return {
				type: t,
				references: n
			}
		}
		case Z.String:
			return {
				type: t,
					value: e.readUnicodeString(0)
			};
		case Z.UnitFloat:
			return {
				type: t,
					unitType: function(e) {
						if (!Object.values(_).includes(e))
							throw new z(`Invalid Unit Float type: ${e}`);
						return e
					}(e.readString(4)),
					value: e.read("f64")
			};
		default:
			throw new F(`Unexpected descriptor type: ${t}`)
	}
}

function Oe(e) {
	const t = e.readString(4);
	switch (t) {
		case Ie.Class:
			return {
				type: t,
					name: e.readUnicodeString(0),
					classId: e.readIdString()
			};
		case Ie.Enumerated:
			return {
				type: t,
					name: e.readUnicodeString(0),
					classId: e.readIdString(),
					typeId: e.readIdString(),
					enumValue: e.readIdString()
			};
		case Ie.Identifier:
			return {
				type: t,
					identifier: e.readString(4)
			};
		case Ie.Index:
			return {
				type: t,
					index: e.read("u32")
			};
		case Ie.Name:
			return {
				type: t,
					name: e.readUnicodeString(0)
			};
		case Ie.Offset:
			return {
				type: t,
					name: e.readUnicodeString(0),
					classId: e.readIdString(),
					offset: e.read("u32")
			};
		case Ie.Property:
			return {
				type: t,
					name: e.readUnicodeString(0),
					classId: e.readIdString(),
					keyId: e.readIdString()
			};
		default:
			throw new E(`Invalid reference type: ${t}`)
	}
}! function(e) {
	e[e.RawData = 0] = "RawData",
		e[e.RleCompressed = 1] = "RleCompressed",
		e[e.ZipWithoutPrediction = 2] = "ZipWithoutPrediction",
		e[e.ZipWithPrediction = 3] = "ZipWithPrediction"
}(we || (we = {})),
function(e) {
	e[e.Red = 0] = "Red",
		e[e.Green = 1] = "Green",
		e[e.Blue = 2] = "Blue",
		e[e.TransparencyMask = -1] = "TransparencyMask",
		e[e.UserSuppliedLayerMask = -2] = "UserSuppliedLayerMask",
		e[e.RealUserSuppliedLayerMask = -3] = "RealUserSuppliedLayerMask"
}(me || (me = {})),
function(e) {
	e[e.Other = 0] = "Other",
		e[e.OpenFolder = 1] = "OpenFolder",
		e[e.CloseFolder = 2] = "CloseFolder",
		e[e.BoundingSection = 3] = "BoundingSection"
}(Se || (Se = {})),
function(e) {
	e[e.Vertical = 0] = "Vertical",
		e[e.Horizontal = 1] = "Horizontal"
}(be || (be = {})),
function(e) {
	e[e.GridAndGuides = 1032] = "GridAndGuides",
		e[e.Slices = 1050] = "Slices"
}(ve || (ve = {})),
function(e) {
	e[e.AutoGenerated = 0] = "AutoGenerated",
		e[e.LayerGenerated = 1] = "LayerGenerated",
		e[e.UserGenerated = 2] = "UserGenerated"
}(xe || (xe = {})),
function(e) {
	e.Class = "Clss",
		e.Enumerated = "Enmr",
		e.Identifier = "Idnt",
		e.Index = "indx",
		e.Name = "name",
		e.Offset = "rele",
		e.Property = "prop"
}(Ie || (Ie = {}));
class Ge {
	composite(e = !0, t = !0) {
		const {
			red: r,
			green: n,
			blue: a,
			alpha: i
		} = this.imageData, {
			width: s,
			height: o
		} = this, c = function(e, t, r, n, a, i) {
			var s, o, c, d;
			const l = e * t;
			if (!(l > 0 && Number.isInteger(l)))
				throw new Error(`Pixel count must be a positive integer, got ${l}`);
			const u = 4 * l,
				h = Math.max(null !== (s = r.data.byteLength) && void 0 !== s ? s : 0, null !== (o = null == a ? void 0 : a.data.byteLength) && void 0 !== o ? o : 0, null !== (c = null == n ? void 0 : n.data.byteLength) && void 0 !== c ? c : 0, null !== (d = null == i ? void 0 : i.data.byteLength) && void 0 !== d ? d : 0);
			if (!(h >= 0 && Number.isInteger(h)))
				throw new Error(`Input region size must be a nonnegative integer, got ${h}`);
			const p = new ArrayBuffer(function(e) {
					let t = 65536;
					for (; t < e;)
						t < 16777216 ? t <<= 1 : t += 16777216;
					return t
				}(u + h)),
				g = new Uint8ClampedArray(p, 0, u),
				f = new Uint8Array(p, u),
				y = function(e, t, r) {
					"use asm";
					const n = new e.Int8Array(r),
						a = new e.Uint8Array(r);

					function i(e, t, r, a) {
						e = e | 0;
						t = t | 0;
						r = r | 0;
						a = a | 0;
						var i = 0;
						var s = 0;
						var o = 0;
						i = e;
						s = a + r | 0;
						for (o = i + t | 0;
							(i | 0) < (o | 0); i = i + 1 | 0) {
							n[s] = n[i];
							s = s + 4 | 0
						}
						return 0
					}

					function s(e, t, r, a) {
						e = e | 0;
						t = t | 0;
						r = r | 0;
						a = a | 0;
						var i = 0;
						var s = 0;
						var o = 0;
						var c = 0;
						var d = 0;
						var l = 0;
						var u = 0;
						i = e;
						s = a + r | 0;
						while ((i | 0) < (e + t | 0)) {
							o = n[i] | 0;
							i = i + 1 | 0;
							if ((o | 0) == -128) {
								continue
							} else if ((o | 0) >= 0) {
								for (d = i + o + 1 | 0;
									(i | 0) < (d | 0); i = i + 1 | 0) {
									n[s] = n[i];
									s = s + 4 | 0
								}
							} else {
								l = n[i] | 0;
								i = i + 1 | 0;
								u = 1 - o | 0;
								for (c = 0;
									(c | 0) < (u | 0); c = c + 1 | 0) {
									n[s] = l;
									s = s + 4 | 0
								}
							}
						}
						return 0
					}

					function o(e, t, r, n) {
						e = e | 0;
						t = t | 0;
						r = r | 0;
						n = n | 0;
						var i = 0;
						var s = 0;
						i = r + e | 0;
						s = e + t | 0;
						while ((i | 0) < (s | 0)) {
							a[i] = n | 0;
							i = i + 4 | 0
						}
						return 0
					}
					return {
						decodeUncompressedChannel: i,
						decodeRleChannel: s,
						setChannelValue: o
					}
				}({
					Int8Array,
					Uint8Array
				}, 0, p);
			return Ue(y, f, g.byteOffset, r, ke(me.Red)),
				Ue(y, f, g.byteOffset, null != n ? n : r, ke(me.Green)),
				Ue(y, f, g.byteOffset, null != a ? a : r, ke(me.Blue)),
				i ? Ue(y, f, g.byteOffset, i, ke(me.TransparencyMask)) : function(e, t, r, n, a) {
					if (!Number.isInteger(n))
						throw new Error("Channel value must be an integer between 0 and 255, got 255");
					const i = e.setChannelValue(t, r, a, n);
					if (0 !== i)
						throw new Error(`Channel update failed (cause: ${i})`)
				}(y, g.byteOffset, g.byteLength, 255, ke(me.TransparencyMask)),
				g.slice()
		}(s, o, r, n, a, i);
		return !0 === e ? Me(c, !0 === t ? 255 * this.composedOpacity : this.opacity) : c
	}
}
class Be extends Ge {
	constructor(e, t) {
		super(),
			this.layerFrame = e,
			this.parent = t,
			this.type = "Layer"
	}
	get name() {
		
		return HieName(this.parent,this.layerFrame.layerProperties.name);
	}
	get width() {
		return this.layerFrame.width
	}
	get height() {
		return this.layerFrame.height
	}
	get top() {
		return this.layerFrame.layerProperties.top
	}
	get left() {
		return this.layerFrame.layerProperties.left
	}
	get opacity() {
		return this.layerFrame.layerProperties.opacity
	}
	get composedOpacity() {
		return this.parent.composedOpacity * (this.opacity / 255)
	}
	get text() {
		return this.layerFrame.layerProperties.text
	}
	get blendMode() {
		return this.layerFrame.layerProperties.blendMode
	}
	get visible() {
		/*
		if(this.layerFrame.layerProperties.visible&2)
		{
			return 'OFF';
		}
		return 'ON';
		*/
		return this.layerFrame.layerProperties.visible.toString(16);
		
	}
	get imageData() {
		const {
			red: e,
			green: t,
			blue: r,
			alpha: n
		} = this.layerFrame;
		return {
			red: e,
			green: t,
			blue: r,
			alpha: n
		}
	}
}
class Ae {
	constructor(e, t, r, n, a) {
		this.origin = e,
			this.left = t,
			this.top = r,
			this.right = n,
			this.bottom = a
	}
}

function Ee(e) {
	if (e.resource.descriptor) {
		const t = e.resource.descriptor.descriptor.items.get("slices");
		if (!t || t.type !== Z.List)
			throw new M('Missing key "slices" in slice descriptor');
		return t.values.reduce(((e, t) => {
			if (t.type !== Z.Descriptor)
				throw new M(`Slice list contains a non-descriptor (type: ${t.type})`);
			return e.push(function(e) {
					const t = function(e) {
							const t = K(e, "origin", Z.Enumerated);
							if ("ESliceOrigin" !== t.enumType)
								throw new M(`Unexpected enum type for slice origin: got "${t.enumType}"`);
							switch (t.enumValue) {
								case "layerGenerated":
									return xe.LayerGenerated;
								case "userGenerated":
									return xe.UserGenerated;
								case "autoGenerated":
									return xe.AutoGenerated;
								default:
									throw new M(`Unexpected enum value for slice origin: got "${t.enumValue}"`)
							}
						}(e),
						r = K(e, "bounds", Z.Descriptor).descriptor,
						n = K(r, "Top ", Z.Integer).value,
						a = K(r, "Left", Z.Integer).value,
						i = K(r, "Btom", Z.Integer).value,
						s = K(r, "Rght", Z.Integer).value;
					return new Ae(t, a, n, s, i)
				}(t.descriptor)),
				e
		}), [])
	}
	throw new M("No slice descriptor in slice resource block")
}
class ze {
	constructor(e, t) {
		this.layerFrame = e,
			this.parent = t,
			this.type = "Group",
			this.children = []
	}
	get name() {
		return HieName(this.parent,this.layerFrame.layerProperties.name);
	}
	get opacity() {
		return this.layerFrame.layerProperties.opacity
	}
	get composedOpacity() {
		return this.parent.composedOpacity * (this.opacity / 255)
	}
	addChild(e) {
		this.children.push(e)
	}
	hasChildren() {
		return 0 !== this.children.length
	}
	freeze() {
		this.children.forEach((e => e.freeze && e.freeze())),
			Object.freeze(this.children)
	}
}
class Ve extends Ge {
	constructor(e) {
		super(),
			this.parsingResult = e,
			this.name = "ROOT",
			this.type = "Psd",
			this.opacity = 255,
			this.composedOpacity = 1,
			this.children = [],
			this.layers = [],
			this.guides = [],
			this.slices = [],
			this.buildTreeStructure();
		for (const t of e.imageResources.resources)
			if (null !== t.resource)
				switch (t.id) {
					case ve.GridAndGuides:
						this.guides = t.resource.guides;
						break;
					case ve.Slices:
						this.slices = Ee(t)
				}
	}
	static parse(e) {
		const t = Re(e);
		return new Ve(t)
	}
	get width() {
		return this.parsingResult.fileHeader.width
	}
	get height() {
		return this.parsingResult.fileHeader.height
	}
	get channelCount() {
		return this.parsingResult.fileHeader.channelCount
	}
	get depth() {
		return this.parsingResult.fileHeader.depth
	}
	get colorMode() {
		return this.parsingResult.fileHeader.colorMode
	}
	get imageData() {
		const {
			compression: e,
			red: t,
			green: r,
			blue: n,
			alpha: a
		} = this.parsingResult.imageData;
		return {
			red: {
				compression: e,
				data: t
			},
			green: r ? {
				compression: e,
				data: r
			} : void 0,
			blue: n ? {
				compression: e,
				data: n
			} : void 0,
			alpha: a ? {
				compression: e,
				data: a
			} : void 0
		}
	}
	buildTreeStructure() {
		const {
			groups: e,
			layers: t,
			orders: r
		} = this.parsingResult.layerAndMaskInfo, n = [this];
		let a = 0,
			i = 0;
		r.forEach((r => {
				var s, o;
				const c = n[n.length - 1];
				switch (r) {
					case "G": {
						const t = e[a],
							r = new ze(t, c);
						n.push(r),
							null === (s = c.children) || void 0 === s || s.push(r),
							a += 1;
						break
					}
					case "L": {
						const e = t[i],
							r = new Be(e, c);
						this.layers.push(r),
							null === (o = c.children) || void 0 === o || o.push(r),
							i += 1;
						break
					}
					case "D":
						n.pop()
				}
			})),
			n.length = 0,
			this.children.forEach((e => e.freeze && e.freeze())),
			Object.freeze(this.children)
	}
}
var LePsd='';
const $e = Ve,
	Ne = "this-is-a-message";
self.addEventListener("message", (({
	data: e
}) => {
	const {
		type: t,
		timestamp: r,
		value: n
	} = e;
	if (function(e) {
			if ("object" != typeof e || null === e || !("type" in e) || !("value" in e) || e.signature !== Ne)
				throw new TypeError(`data is not an ExampleMessage (got ${e})`);
			const t = e.type;
			switch (t) {
				case "Layer":
				case "MainImageData":
				case "ParseData":
					return;
				default:
					(e => {
						throw new TypeError(`Unexpected ExampleMessage type: ${e}`)
					})(t)
			}
		}(e),
		console.log("It took %d ms to send this message (main �� worker, type: %o)", Date.now() - r, t),
		"ParseData" === t) {
		console.time("Parse PSD file");
		const e = $e.parse(n);
		console.timeEnd("Parse PSD file"),
			LePsd=e,
			console.log(e),
			e.layers.forEach(((e, t) => {
				console.time(`Compositing layer ${t}`);
				const r = e.composite(!0, !0);
				console.timeEnd(`Compositing layer ${t}`),
					self.postMessage(function(e, t) {
						return {
							type: "Layer",
							value: t,
							signature: Ne,
							timestamp: Date.now()
						}
					}(0, {
						pixelData: r,
						composedOpacity: e.composedOpacity,
						blendMode: e.blendMode,
						visible: e.visible,
						name: e.name,
						left: e.left,
						top: e.top,
						width: e.width,
						height: e.height
					}), [r.buffer])
			}))
	} else
		console.error("Worker received a message that it cannot handle: %o", e)
}))

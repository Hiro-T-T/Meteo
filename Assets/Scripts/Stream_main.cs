//
// Stream - line particle system
//
using UnityEngine;
using System.Collections;

namespace Kvant
{
    [ExecuteInEditMode, AddComponentMenu("Kvant/Stream")]
    public class Stream_main : MonoBehaviour
    {

        public AudioClip audioClip;
        AudioSource audioSource;
        private bool se_on = false;

        int knobNumber1 = 16;
	     int knobNumber2 = 17;
	     int knobNumber3 = 18;
         int knobNumber4 = 19;
         int knobNumber5 = 20;
         int knobNumber6 = 21;
         int knobNumber7 = 22;
         int knobNumber8 = 23;
         int knobNumber9 = 0;

        #region Parameters Exposed To Editor

        [SerializeField]
        int _maxParticles = 35000;

        [SerializeField]
        Vector3 _emitterPosition = Vector3.forward * 20;

        [SerializeField]
        Vector3 _emitterSize = Vector3.one * 40;

        [SerializeField, Range(0, 1)]
        float _throttle = 1.0f;

        [SerializeField]
        Vector3 _direction = -Vector3.forward;

        [SerializeField]
        float _minSpeed = 5.0f;

        [SerializeField]
        float _maxSpeed = 10.0f;

        [SerializeField, Range(0, 1)]
        float _spread = 0.2f;

        [SerializeField]
        float _noiseAmplitude = 0.1f;

        [SerializeField]
        float _noiseFrequency = 0.2f;

        [SerializeField]
        float _noiseSpeed = 1.0f;

        [SerializeField, ColorUsage(true, true, 0, 8, 0.125f, 3)]
        Color _color = Color.white;

        [SerializeField]
        float _tail = 1.0f;

        [SerializeField]
        int _randomSeed = 0;

        [SerializeField]
        bool _debug;

        #endregion

        #region Public Properties

        

        public int maxParticles {
            // Returns the actual number of particles.
            get { return BufferWidth * BufferHeight; }
        }

        public float throttle {
            get { return _throttle; }
            set { _throttle = value; }
        }

        public Vector3 emitterPosition {
            get { return _emitterPosition; }
            set { _emitterPosition = value; }
        }

        public Vector3 emitterSize {
            get { return _emitterSize; }
            set { _emitterSize = value; }
        }

        public Vector3 direction {
            get { return _direction; }
            set { _direction = value; }
        }

        public float minSpeed {
            get { return _minSpeed; }
            set { _minSpeed = value; }
        }

        public float maxSpeed {
            get { return _maxSpeed; }
            set { _maxSpeed = value; }
        }

        public float spread {
            get { return _spread; }
            set { _spread = value; }
        }

        public float noiseAmplitude {
            get { return _noiseAmplitude; }
            set { _noiseAmplitude = value; }
        }

        public float noiseFrequency {
            get { return _noiseFrequency; }
            set { _noiseFrequency = value; }
        }

        public float noiseSpeed {
            get { return _noiseSpeed; }
            set { _noiseSpeed = value; }
        }

        public Color color {
            get { return _color; }
            set { _color = value; }
        }

        public float tail {
            get { return _tail; }
            set { _tail = value; }
        }

         

        
        #endregion

        #region Shader And Materials

        [SerializeField] Shader _kernelShader;
        [SerializeField] Shader _lineShader;
        [SerializeField] Shader _debugShader;

        Material _kernelMaterial;
        Material _lineMaterial;
        Material _debugMaterial;

        #endregion

        #region Private Variables And Objects

        RenderTexture _particleBuffer1;
        RenderTexture _particleBuffer2;
        Mesh _mesh;
        bool _needsReset = true;

        #endregion

        #region Private Properties

        int BufferWidth { get { return 256; } }

        int BufferHeight {
            get {
                return Mathf.Clamp(_maxParticles / BufferWidth + 1, 1, 127);
            }
        }

        static float deltaTime {
            get {
                return Application.isPlaying && Time.frameCount > 1 ? Time.deltaTime : 1.0f / 10;
            }
        }

        #endregion

        #region Resource Management

        public void NotifyConfigChange()
        {
            _needsReset = true;
        }

        Material CreateMaterial(Shader shader)
        {
            var material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
            return material;
        }

        RenderTexture CreateBuffer()
        {
            var buffer = new RenderTexture(BufferWidth, BufferHeight, 0, RenderTextureFormat.ARGBFloat);
            buffer.hideFlags = HideFlags.DontSave;
            buffer.filterMode = FilterMode.Point;
            buffer.wrapMode = TextureWrapMode.Repeat;
            return buffer;
        }

        Mesh CreateMesh()
        {
            var Nx = BufferWidth;
            var Ny = BufferHeight;

            // Create vertex arrays.
            var VA = new Vector3[Nx * Ny * 2];
            var TA = new Vector2[Nx * Ny * 2];

            var Ai = 0;
            for (var x = 0; x < Nx; x++)
            {
                for (var y = 0; y < Ny; y++)
                {
                    VA[Ai + 0] = new Vector3(1, 0, 0);
                    VA[Ai + 1] = new Vector3(0, 0, 0);

                    var u = (float)x / Nx;
                    var v = (float)y / Ny;
                    TA[Ai] = TA[Ai + 1] = new Vector2(u, v);

                    Ai += 2;
                }
            }

            // Index array.
            var IA = new int[VA.Length];
            for (Ai = 0; Ai < VA.Length; Ai++) IA[Ai] = Ai;

            // Create a mesh object.
            var mesh = new Mesh();
            mesh.hideFlags = HideFlags.DontSave;
            mesh.vertices = VA;
            mesh.uv = TA;
            mesh.SetIndices(IA, MeshTopology.Lines, 0);
            mesh.Optimize();

            // Avoid being culled.
            mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000);

            return mesh;
        }

        void UpdateKernelShader()
        {
            var m = _kernelMaterial;

            m.SetVector("_EmitterPos", _emitterPosition);
            m.SetVector("_EmitterSize", _emitterSize);

            var dir = new Vector4(_direction.x, _direction.y, _direction.z, _spread);
            m.SetVector("_Direction", dir);

            m.SetVector("_SpeedParams", new Vector2(_minSpeed, _maxSpeed));

            if (_noiseAmplitude > 0)
            {
                var np = new Vector3(_noiseFrequency, _noiseAmplitude, _noiseSpeed);
                m.SetVector("_NoiseParams", np);
                m.EnableKeyword("NOISE_ON");
            }
            else
            {
                m.DisableKeyword("NOISE_ON");
            }

            var life = 2.0f;
            m.SetVector("_Config", new Vector4(_throttle, life, _randomSeed, deltaTime));
        }

        void ResetResources()
        {
            // Mesh object.
            if (_mesh == null) _mesh = CreateMesh();

            // Particle buffers.
            if (_particleBuffer1) DestroyImmediate(_particleBuffer1);
            if (_particleBuffer2) DestroyImmediate(_particleBuffer2);

            _particleBuffer1 = CreateBuffer();
            _particleBuffer2 = CreateBuffer();

            // Shader materials.
            if (!_kernelMaterial) _kernelMaterial = CreateMaterial(_kernelShader);
            if (!_lineMaterial)   _lineMaterial   = CreateMaterial(_lineShader);
            if (!_debugMaterial)  _debugMaterial  = CreateMaterial(_debugShader);

            // Warming up.
            UpdateKernelShader();
            InitializeAndPrewarmBuffers();

            _needsReset = false;
        }

        void InitializeAndPrewarmBuffers()
        {
            // Initialization.
            Graphics.Blit(null, _particleBuffer2, _kernelMaterial, 0);

            // Execute the kernel shader repeatedly.
            for (var i = 0; i < 8; i++) {
                Graphics.Blit(_particleBuffer2, _particleBuffer1, _kernelMaterial, 1);
                Graphics.Blit(_particleBuffer1, _particleBuffer2, _kernelMaterial, 1);
            }
        }

        #endregion

        #region MonoBehaviour Functions

        void Reset()
        {
            _needsReset = true;
        }

        void OnDestroy()
        {
            if (_mesh) DestroyImmediate(_mesh);
            if (_particleBuffer1) DestroyImmediate(_particleBuffer1);
            if (_particleBuffer2) DestroyImmediate(_particleBuffer2);
            if (_kernelMaterial)  DestroyImmediate(_kernelMaterial);
            if (_lineMaterial)    DestroyImmediate(_lineMaterial);
            if (_debugMaterial)   DestroyImmediate(_debugMaterial);
        }
        private StageManager stage_manager;
        private Spawn spawn;
        private int backgroundTime = 0;

        void Start()
        {
            stage_manager = GameObject.Find("GameController").GetComponent<StageManager>();
            spawn = GameObject.Find("spawn_object").GetComponent<Spawn>();
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
        }

        void Update()
        {
           
            //  transform.Rotate(0.0f, 0.0f, stage_manager.game_rotate);
            if(stage_manager.backgroundFlag == true)
            {
                backgroundTime++;
                if (backgroundTime < 180)
                {
                    audioSource.PlayOneShot(audioClip);
                    _maxSpeed++;
                    _tail = 300 / _maxSpeed;
                    if (_maxSpeed >= 1000) _maxSpeed = 1000;
                }
                else
                {
                    _maxSpeed--;
                    _tail = 300 / maxSpeed;
                    if (_maxSpeed <= (3 * spawn.spawn_count))
                    {
                        _maxSpeed = (3 * spawn.spawn_count);
                        spawn.spawn_count++;
                        spawn.interval = 0;
                        //spawn.SendMessage("Spawn_p");
                        backgroundTime = 0;
                        stage_manager.backgroundFlag = false;
                    }
                }

                if (stage_manager.itemFlag == true)
                {
                    backgroundTime++;
                    if (backgroundTime < 180)
                    {
                        audioSource.PlayOneShot(audioClip);
                        _maxSpeed++;
                        _tail = 300 / _maxSpeed;
                        if (_maxSpeed >= 1000) _maxSpeed = 1000;
                    }
                    else
                    {
                        _maxSpeed--;
                        _tail = 300 / maxSpeed;
                        if (backgroundTime > 360)
                        {
                            spawn.interval = 0;
                            //spawn.SendMessage("Spawn_p");
                            backgroundTime = 0;
                            stage_manager.itemFlag = false;
                        }
                    }

                }
            }
            
            if (_needsReset) ResetResources();

            UpdateKernelShader();

            if (Application.isPlaying)
            {
                // Swap the particle buffers.
                var temp = _particleBuffer1;
                _particleBuffer1 = _particleBuffer2;
                _particleBuffer2 = temp;

                // Execute the kernel shader.
                Graphics.Blit(_particleBuffer1, _particleBuffer2, _kernelMaterial, 1);
            }
            else
            {
                InitializeAndPrewarmBuffers();
            }

            // Draw particles.
            _lineMaterial.SetTexture("_ParticleTex1", _particleBuffer1);
            _lineMaterial.SetTexture("_ParticleTex2", _particleBuffer2);
            _lineMaterial.SetColor("_Color", _color);
            _lineMaterial.SetFloat("_Tail", _tail / deltaTime / 60);
            Graphics.DrawMesh(_mesh, transform.position, transform.rotation, _lineMaterial, 0);
        }

        void OnGUI()
        {
            if (_debug && Event.current.type.Equals(EventType.Repaint))
            {
                if (_debugMaterial && _particleBuffer2)
                {
                    var rect = new Rect(0, 0, 256, 64);
                    Graphics.DrawTexture(rect, _particleBuffer2, _debugMaterial);
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(_emitterPosition, _emitterSize);
        }

        #endregion
    }
}

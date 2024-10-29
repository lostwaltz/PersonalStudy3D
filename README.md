PersonalStudy3D
3D 공간의 유니티 엔진을 위한 개인 프로젝트입니다.

조작법
이동: W, A, S, D
점프: SPACE
인벤토리: TAB
상호작용: E
<details> <summary>토글 접기/펼치기</summary>
구현 기능
기본 이동 및 점프 Input System, Rigidbody ForceMode (난이도: ★★☆☆☆)

InputSystem의 Invoke를 통해 이벤트를 트리거하고 Movement에서 구독해 입력값에 따른 변수를 초기화하고, 상태에 따라 실제 움직일 함수를 호출합니다.
체력바 UI UI (난이도: ★★☆☆☆)

체력바는 ValueSystem을 상속받아 Update를 추상화하고, VitalController에서 추상화된 ValueSystem의 함수를 제공, 호출합니다.
동적 환경 조사 Raycast, UI (난이도: ★★★☆☆)

Interaction 컴포넌트에서 레이 충돌을 검사하고, 충돌한 오브젝트의 IInteractable 인터페이스를 통해 추상화 함수를 호출합니다.
점프대 Rigidbody ForceMode (난이도: ★★★☆☆) (Class JumpPad)

OnCollisionEnter를 통해 충돌 시 Rigidbody의 AddForce를 호출합니다.
아이템 데이터 ScriptableObject (난이도: ★★★☆☆)

ItemSO를 상속받는 ConsumableSO, EquipmentSO를 생성했습니다.
아이템 사용 Coroutine (난이도: ★★★☆☆)

ItemSO의 Use 메소드를 추상화하고, 상속받는 SO에서 Use를 구현하도록 했습니다.
3️⃣ 도전 기능 가이드
🚨 모든 필수 기능 구현을 마친 후 선택적으로 도전하는 기능입니다.

추가 UI (난이도: ★★☆☆☆)

스태미너 UI, 인벤토리 UI를 제작했습니다. 동일하게 ValueSystem을 상속받아 Update를 추상화하고, VitalController에서 추상화된 ValueSystem의 함수를 제공, 호출합니다.
인벤토리 UI는 단순하게 구현된 시스템으로, 개편의 필요성을 느끼고 있습니다.
3인칭 시점 (난이도: ★★★☆☆)

Movement의 ApplyLook 메소드에서 3인칭 시점이 될 수 있도록 계산하고 있습니다.
움직이는 플랫폼 구현 (난이도: ★★★☆☆)

Platform 컴포넌트를 통해 유니티에서 제공하는 Mathf.PingPong을 사용해 0부터 1까지 값을 얻고 해당 값으로 보간했습니다. OnCollisionEnter 시 플레이어의 계층 구조를 Platform의 자식으로 바꿉니다.
벽 타기 및 매달리기 (난이도: ★★★★☆)

플레이어의 앞 방향으로 Raycast를 수행하고, 바라보는 곳 앞에 사다리가 존재하면 Climb 상태로 전이됩니다. 해당 상태에서는 ApplyMove 대신 ApplyClimb을 호출해 진행했습니다. AddForce.Acceleration을 사용해 구현해봤으나 자연스러운 움직임 구현이 어려워 입력값을 Y값으로 치환해 구현했습니다.
다양한 아이템 구현 (난이도: ★★★★☆)

ConsumableSO에서 Use 메소드를 재정의해 Type에 따라 switch 분기를 나누었습니다. 많은 분기가 생길 경우 다형성을 통해 리팩토링해야 함을 느끼고 있습니다.
장비 장착 (난이도: ★★★★☆)

Equipment, Equip 클래스를 통해 구현했습니다. 장착 시 EquipmentSO를 받습니다.
레이저 트랩 (난이도: ★★★★☆)

Raycast를 사용해 특정 구간을 레이로 감시하고, 플레이어가 레이저를 통과하면 Text를 활성화하고 코루틴을 통해 유지시켰습니다.
상호작용 가능한 오브젝트 표시 (난이도: ★★★★★)

Interaction에서 IInteractable의 OnHitRay 메소드를 통해 호출되고, Cannon에서 상속받은 IInteractable의 OnHitRay를 구현하면서 WorldCanvas의 Text 위치를 상호작용 중인 오브젝트의 머리 위에 위치하게 했습니다.
플랫폼 발사기 (난이도: ★★★★★)

Cannon에서 구현한 IInteractable의 OnInteract에서 발사를 구현했습니다.
현재 프로젝트 구조상 ApplyMove가 호출될 때 입력값이 없으면 Velocity를 0으로 초기화하는데, 입력값이 없을 때 항상 호출되어 AddForce를 통해 발사돼도 X, Z축 힘이 초기화되는 문제가 있었습니다.
상태 패턴을 통해 InteractionState를 만들고, 해당 상태에서 ApplyMove를 호출하지 않도록 해결했습니다.
마지막으로 구현에 대한 의문점은 InteractionState에 진입하기 위한 조건 검사에 있어서 더 좋은 방법이 없을까입니다.
플레이어가 IInteractable 오브젝트를 찾아 이벤트를 구독하는 것은 동적으로 생성될 객체에 문제가 있고, 동적으로 생성될 객체가 플레이어를 찾아 직접 이벤트를 연결해주는 것도 깔끔하지 않은 것 같습니다.
현재 구조는 OnInteract 호출 시 플레이어의 상태를 외부에서 강제로 TransitionTo 하는 것인데, 항상 프로젝트를 진행하면서 외부에서 값 수정이 빈번하면 값의 신뢰도, 순서, 변수 제어가 어려워진다는 것을 알고있기에 어떤 방법이 가장 확실한지 아직 감을 잡지 못했지만, 학습하고 수정하도록 하겠습니다.


미구현

발전된 AI (난이도: ★★★★★)
</details>

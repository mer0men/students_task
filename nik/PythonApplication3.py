import tcod  as t




screen_width = 80
screen_height = 50

# �������


key = t.Key()
mouse = t.Mouse()

#  ����� 
class player:
    a = 10
    b =  5
    def move(self, a, b):
         if not obstacle(a, b):
            self.a = a
            self.b = b

   
MAP = [
    "          ################### ",  
    "###########              ######",
    "#                           ##",
    "#        T                   #",
    "#                            #",
    "#          T        T        #",
    "#                            #",
    "#          T                 #",
    "##                        ####",
    "##############################",
]  

def  obstacle(a, b):
     if b > len(MAP) or b < 0:
         return True
     if a > len(MAP[0]) or a < 0:
         return True
     el = MAP[b][a]
     if el ==''or el == '\t' or  el == '.':
         return  False
     else:
         return True

    









def set_map(a, b, d):
    if b > len(MAP) or b < 0:
        return
    if a > len(MAP[0]) or  a < 0:
        return
    MAP[b] = MAP[b][:a]+ d + MAP[b][a+1:]
    

def map_draw():
    for b in  range (len(MAP)):
        for a in range (len(MAP[0])):
          cchar(a, b, MAP[b][a])            
#  ����  -  ���  ����
def game():
    player == player ()
  
    #  �������
    t.console_init_root(screen_width, screen_height, "My Game")

    while not  t.console_is_window_closed():
        t.console_set_default_foreground(0, t.white)
        t.sys_check_for_event(t.EVENT_KEY_PRESS, key, mouse)

        # ���������� �����
        map_draw()
        cchar(player.a, player.b, '@')
        


      
        t.console_flush()

        cclear()
     
 
        if key.vk == t.KEY_ESCAPE: 
            return

        if key.vk == t.KEY_UP:
            player.b-=1       
                             
                  
        elif key.vk == t.KEY_DOWN:
            player.b+=1
        if key.vk == t.KEY_LEFT:
            player.a-=1
        elif key.vk == t.KEY_RIGHT:
            player.a+=1

            
        if key.vk == t.KEY_SPACE:
            set_map(player.a, player.b, '.')
            
        if key.vk == t.KEY_CONTROL:
            set_map(player.a, player.b, 'o')

        if key.vk == t.KEY_ENTER:
            set_map(player.a,player.b, '')
            set_map(player.a-1,player.b, '')
            set_map(player.a+1,player.b, '')
            set_map(player.a,player.b-1, '')
            set_map(player.a,player.b+1, '')

   
    
    
    
      
def  cchar(a, b, d): 
     t.console_put_char(0, a, b, ord(d))

def  cprint(a, b, txt):
     t.console_print(0, a, b, txt)

def cclear():
    t.console_clear(0)

if __name__ == '__main__':
    game()
